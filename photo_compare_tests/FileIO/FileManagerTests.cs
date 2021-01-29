using Xunit;
using photo_compare.FileIO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace photo_compare.FileIO.Tests
{
    public class FileManagerTests
    {
        private string testFilesLocation = "./Utilities/TestFiles";
        private readonly IList<string> _fileList;
        private readonly IFileManager _fileManager;

        public FileManagerTests()
        {
            _fileManager = new FileManager();

            _fileList = _fileManager.GetImageFileListFromFolder(testFilesLocation);
        }

        [Fact()]
        public void GetImageFileListFromFolderTest()
        {
            Assert.NotEqual(10, _fileList.Count);

            Assert.Equal(5, _fileList.Count);

            var mew = _fileList.Any(f => f.EndsWith("mew.jpg"));

            Assert.True(mew);

            var newTextDocument = _fileList.Any(f => f.EndsWith(".txt"));

            Assert.False(newTextDocument);
        }

        
    }
}