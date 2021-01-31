using Xunit;
using photo_compare.FileIO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using photo_compare.Models;

namespace photo_compare.FileIO.Tests
{
    public class FileManagerTests
    {
        private string testFilesLocation = "./TestFiles";
        private readonly IFileManager _fileManager;

        public FileManagerTests()
        {
            _fileManager = new FileManager();
        }

        [Fact()]
        public void GetImageFileListFromFolderTest()
        {
            var fileList = _fileManager.GetImageFileListFromFolder(testFilesLocation);

            Assert.NotEqual(10, fileList.Count);

            Assert.Equal(5, fileList.Count);

            var mew = fileList.Any(f => f.Name.EndsWith("mew.jpg"));

            Assert.True(mew);

            var newTextDocument = fileList.Any(f => f.Name.EndsWith(".txt"));

            Assert.False(newTextDocument);
        }

        [Fact()]
        public void DoesFolderExistTest()
        {
            var checkCDrive = _fileManager.DoesFolderExist("c:\\");

            Assert.True(checkCDrive);

            var checkJibberishFolder = _fileManager.DoesFolderExist("C:\\itisveryunlikelythatIexist");

            Assert.False(checkJibberishFolder);
        }
    }
}