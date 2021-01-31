using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using photo_compare.ConsoleIO;
using photo_compare.ImageIO;
using photo_compare.Models;

namespace photo_compare.FileIO
{
    public class FileManager : IFileManager
    {
        private readonly string[] _imageExts = new[] { ".jpg", ".jpeg", ".bmp", ".gif", ".png" };

        private readonly IConsolePrinter _consolePrinter;
        private readonly IImageManager _imageManager;

        public FileManager()
        {
            _consolePrinter = new ConsolePrinter();
            _imageManager = new ImageManager();
        }

        public bool DoesFolderExist(string folderPath)
        {
            return Directory.Exists(folderPath);
        }

        public IList<ImageFile> GetImageFileListFromFolder(string folderPath)
        {
            IList<ImageFile> result = new List<ImageFile>();

            try
            {
                var directoryFiles = Directory
                    .EnumerateFiles(folderPath, "*", SearchOption.AllDirectories)
                    .Where(f => _imageExts.Any(x => f.ToLower().EndsWith(x.ToLower())))
                    .Select(x => new FileInfo(x))
                    ;

                foreach (var file in directoryFiles)
                {
                    var theImage = _imageManager.CreateBitmapFromFilePath(file.FullName);

                    result.Add(new ImageFile()
                    {
                        Name = file.Name,
                        FullPath = file.DirectoryName,
                        Image =  theImage
                    });
                }

                

                return result;

            }
            catch (Exception e)
            {
                _consolePrinter.PrintError("There was an error reading the folder", e);

                throw;
            }
        }

    }
}
