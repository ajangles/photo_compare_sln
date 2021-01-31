using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace photo_compare.Models
{
    public class ImageFile
    {
        public string Name { get; set; }
        public string FullPath { get; set; }
        public IList<ImageFile> SimilarImages { get; set; }
        public Bitmap Image { get; set; }

        public ImageFile()
        {
            SimilarImages = new List<ImageFile>();
        }

    }
}
