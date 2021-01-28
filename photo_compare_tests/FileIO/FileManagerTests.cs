using Xunit;
using photo_compare.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace photo_compare.FileIO.Tests
{
    public class FileManagerTests
    {
        private string testFilesLocation = "./Utilities/TestFiles";

        private IFileManager fileManager;

        public FileManagerTests()
        {
            fileManager = new FileManager();
        }

        [Fact()]
        public void GetImageFileListFromFolderTest()
        {
            var fileList = fileManager.GetImageFileListFromFolder(testFilesLocation);

            Assert.NotEqual(10, fileList.Count);

            Assert.Equal(5, fileList.Count);

            var mew = fileList.Any(f => f.EndsWith("mew.jpg"));

            Assert.True(mew);

            var newTextDocument = fileList.Any(f => f.EndsWith(".txt"));

            Assert.False(newTextDocument);
        }

        [Fact()]
        public void CompareTwoFilesTest()
        {
            throw new NotImplementedException();
        }
    }
}