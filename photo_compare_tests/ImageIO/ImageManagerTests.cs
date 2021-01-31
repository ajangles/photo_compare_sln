using Xunit;
using photo_compare.ImageIO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace photo_compare.ImageIO.Tests
{
    public class ImageManagerTests
    {
        private readonly IImageManager _imageManager;

        public ImageManagerTests()
        {
            _imageManager = new ImageManager();
        }


        [Fact()]
        public void CompareTwoImagesTest()
        {
            var scaryBearOne = new Bitmap(@"./TestFiles/scarybear.JPG");
            var scaryBearTwo = new Bitmap(@"./TestFiles/scarybear2smallres.jpg");

            Assert.NotNull(scaryBearOne);
            Assert.NotNull(scaryBearTwo);

            var expectedBearResult = 6;

            var bearResult = _imageManager.CompareTwoImages(scaryBearOne, scaryBearTwo);
            var bearResult2 = _imageManager.CompareTwoImages(scaryBearTwo, scaryBearOne);

            Assert.Equal(expectedBearResult, bearResult);
            Assert.Equal(expectedBearResult, bearResult2);


            var elephantOne = new Bitmap(@"./TestFiles/elephant1.jpg");
            var elephantTwo = new Bitmap(@"./TestFiles/elephant2.jpg");

            Assert.NotNull(elephantOne);
            Assert.NotNull(elephantTwo);

            var expectedElephantResult = 0;

            var elephantResult = _imageManager.CompareTwoImages(elephantOne, elephantTwo);
            
            Assert.Equal(expectedElephantResult, elephantResult);


            var cat = new Bitmap(@"./TestFiles/mew.jpg");

            Assert.NotNull(cat);

            var expectedCatElephantResult = 98;
            var catElephantResult = _imageManager.CompareTwoImages(cat, elephantOne);

            Assert.Equal(expectedCatElephantResult, catElephantResult);
        }

    }
}