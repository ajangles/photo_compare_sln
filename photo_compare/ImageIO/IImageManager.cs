using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

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
    }
}
