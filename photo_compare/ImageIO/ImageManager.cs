using System;
using System.Drawing;
using photo_compare.ConsoleIO;

namespace photo_compare.ImageIO
{
    public class ImageManager : IImageManager
    {
        const int Tolerance = 10; //tolerance for comparing pixels from each image, 10 appears to work well but this could be adapted in the future
        const int ErrorCode = 666;
        private readonly IConsolePrinter _consolePrinter;

        public ImageManager()
        {
            _consolePrinter = new ConsolePrinter();
        }

        public int CompareTwoImages(Bitmap inputImageOne, Bitmap inputImageTwo)
        {
            //Adapted from https://stackoverflow.com/a/21790555/3749506

            if (inputImageOne == null || inputImageTwo == null)
            {
                return ErrorCode;
            }

            try
            {
                Bitmap imageOne;
                Bitmap imageTwo;

                // lock objects for multi threading
                lock (inputImageOne)
                {
                    imageOne = new Bitmap(inputImageOne, new Size(128, 128));
                }

                lock (inputImageTwo)
                {
                    imageTwo = new Bitmap(inputImageTwo, new Size(128, 128));
                }

                //Create / merge images to the same height/width

                int imageOneSize = imageOne.Width * imageOne.Height;
                int imageTwoSize = imageTwo.Width * imageTwo.Height;

                Bitmap imageThree;

                if (imageOneSize > imageTwoSize)
                {
                    imageOne = new Bitmap(imageOne, imageTwo.Size);
                    imageThree = new Bitmap(imageTwo.Width, imageTwo.Height);
                }
                else
                {
                    imageOne = new Bitmap(imageOne, imageTwo.Size);
                    imageThree = new Bitmap(imageTwo.Width, imageTwo.Height);
                }

                //compare pixels for similarity 

                for (int x = 0; x < imageOne.Width; x++)
                {
                    for (int y = 0; y < imageOne.Height; y++)
                    {
                        Color colorOne = imageOne.GetPixel(x, y);
                        Color colorTwo = imageTwo.GetPixel(x, y);
                        int r = colorOne.R > colorTwo.R ? colorOne.R - colorTwo.R : colorTwo.R - colorOne.R;
                        int g = colorOne.G > colorTwo.G ? colorOne.G - colorTwo.G : colorTwo.G - colorOne.G;
                        int b = colorOne.B > colorTwo.B ? colorOne.B - colorTwo.B : colorTwo.B - colorOne.B;
                        imageThree.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }

                var difference = 0;

                for (var x = 0; x < imageOne.Width; x++)
                {
                    for (var y = 0; y < imageOne.Height; y++)
                    {
                        var colorOne = imageThree.GetPixel(x, y);
                        var media = (colorOne.R + colorOne.G + colorOne.B) / 3;
                        if (media > Tolerance)
                            difference++;
                    }
                }

                //return likely hood they're the same

                var usedSize = imageOneSize > imageTwoSize ? imageTwoSize : imageOneSize;
                var result = difference * 100 / usedSize;

                return result;
            }
            catch (Exception e)
            {
                _consolePrinter.PrintError("Error comparing image files", e);
                throw;
            }
        }

        public Bitmap CreateBitmapFromFilePath(string filePath)
        {
            try
            {
                var image = new Bitmap(filePath);
                return image;

            }
            catch (Exception e)
            {
                _consolePrinter.PrintError("There was an error importing the image " + filePath, e);
                return null;
            }

        }
    }
}
