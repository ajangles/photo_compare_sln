using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using photo_compare.ConsoleIO;
using photo_compare.FileIO;
using photo_compare.ImageIO;
using photo_compare.Models;

namespace photo_compare
{
    public class RunMe : IRunMe
    {
        private readonly IFileManager _fileManager;
        private readonly IImageManager _imageManager;
        private readonly IConsolePrinter _consolePrinter;

        public RunMe()
        {
            _fileManager = new FileManager();
            _imageManager = new ImageManager();
            _consolePrinter = new ConsolePrinter();
        }

        public void Start()
        {
            _consolePrinter.PrintWelcomeMessage();

            var filesNotRead = true;

            //Get input from user 
            while (filesNotRead)
            {
                try
                {
                    var folderPath = _consolePrinter.GetEntryFromUser(
                        "Please enter the location of the merged and duplicated photos, or Q  and enter to quit \r\n\nEg c:\\merged photos\\ "
                        );

                    if (folderPath.ToLower() == "q")
                    {
                        Stop();
                    }

                    var folderExists = _fileManager.DoesFolderExist(folderPath);

                    while (!folderExists)
                    {
                        //Get valid input from user

                        folderPath = _consolePrinter.GetEntryFromUser("The folder entered was not found, please check and try again, or press Q and enter to quit"
                         );

                        if (folderPath.ToLower() == "q")
                        {
                            Stop();
                        }

                        folderExists = _fileManager.DoesFolderExist(folderPath);
                    }

                    _consolePrinter.PrintMessage(
                        "Starting read of folder and sub folders, any import errors will be listed below, but not stop the process\r\n"
                        );

                    var files = _fileManager.GetImageFileListFromFolder(folderPath);

                    filesNotRead = false;
                    
                    var fileCount = files.Count;

                    var userNotResponded = true;

                    while (userNotResponded)
                    {
                        var response = _consolePrinter.GetEntryFromUser(
                            "\r\n" + 
                            $"{fileCount:##,###}" + 
                            " files have been found, high file counts can take a long time to sort for duplicates and will impede your system performance ! \r\nDo you want to continue ? (Y\\N)"
                            );

                        if (response.ToLower() == "y" || response.ToLower() == "n")
                        {
                            userNotResponded = false;

                            if (response.ToLower() == "y")
                            {
                                var duplicates = FindImages(_imageManager, files);

                                DisplayDuplicates(duplicates);
                            }
                        }
                    }

                    
                }
                catch (Exception e)
                {
                    _consolePrinter.PrintError("", e);
                }
            }
            
            _consolePrinter.PrintMessage("Good bye!");
        }

        private IList<ImageFile> FindImages(IImageManager imageManager, IList<ImageFile> fileList)
        {
            //Find the images that resemble each other
            Parallel.ForEach(fileList, file =>
            {
                foreach (var imageFile in fileList)
                {
                    if (file.FullPath != imageFile.FullPath)
                    {
                        lock (file)
                        {
                            var comparisonResult = imageManager.CompareTwoImages(file.Image, imageFile.Image);
                            if (comparisonResult < 10)
                            {
                                file.SimilarImages.Add(imageFile);
                            }
                        }
                    }
                }

            });

            var imagesWithDuplicates = fileList.Where(f => f.SimilarImages.Count > 0).ToList();

            return imagesWithDuplicates;
        }

        private void DisplayDuplicates(IList<ImageFile> images)
        {
            //Alert user that images were found, then print to the console
            
            //TODO adapt for lists > the buffer of the console, eg write to file, pagination. 

            var imagesWithDuplicatesCount = images.Count();

            _consolePrinter.GetEntryFromUser(
                $"{imagesWithDuplicatesCount:##,###}" + 
                " images have been found with duplicates, press enter to print them out to the console."
                );

            foreach (var imageFile in images)
            {
                if (imageFile.SimilarImages.Count > 0)
                {
                    _consolePrinter.PrintSimilarImagesDetails(imageFile);

                }
            }
        }

        private void Stop()
        {
            //Servus
            Environment.Exit(0);
        }
    }
}
