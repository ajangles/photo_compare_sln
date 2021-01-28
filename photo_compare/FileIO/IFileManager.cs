using System;
using System.Collections.Generic;
using System.Text;

namespace photo_compare.FileIO
{
    public interface IFileManager
    {
        /// <summary>
        /// Will return a list of images from the supplied folder and its subfolders
        /// Searched for file extensions are configured in the config file
        /// </summary>
        /// <param name="folderPath">Folder Path</param>
        /// <returns>List of File names inc paths</returns>
        public IList<string> GetImageFileListFromFolder(string folderPath);

        /// <summary>
        /// Compares the two supplied images
        /// </summary>
        /// <param name="image1"></param>
        /// <param name="image2"></param>
        /// <returns>True if they are considered to be comparable</returns>
        public bool CompareTwoFiles(string image1, string image2);
    }
}
