using System;
using System.Collections.Generic;
using System.Text;
using photo_compare.Models;

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
        public IList<ImageFile> GetImageFileListFromFolder(string folderPath);

        /// <summary>
        /// Checks if a folder exists or not
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public bool DoesFolderExist(string folderPath);
    }
}
