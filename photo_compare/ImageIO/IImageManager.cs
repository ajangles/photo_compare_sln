using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using photo_compare.Models;

namespace photo_compare.ImageIO
{
    public interface IImageManager
    {
        /// <summary>
        /// Compares the two supplied images
        /// </summary>
        /// <param name="image1"></param>
        /// <param name="image2"></param>
        /// <returns>True if they are considered to be comparable</returns>
        public int CompareTwoImages(Bitmap image1, Bitmap image2);
        
        /// <summary>
        /// Creates a list of bitmaps from a provided list of file locations
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public Bitmap CreateBitmapFromFilePath(string filePath);
    }
}
