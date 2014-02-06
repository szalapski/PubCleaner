using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using System.Collections.Generic;

namespace Szalapski.PubCleaner.Lib.Tests.Unit {
    [TestClass]
    class CleanerTests {

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Clean_RunWithNullFiles_ThrowsException() {
            var mockKindle = new Mock<IPeriodicalStore>();
            var sut = new SingleCleaner(mockKindle.Object);
            var result = sut.Clean();
        }

        [TestMethod]
        public void Clean_RunWithNoFiles_Succeeds() {
            //arrange
            var mockKindle = new Mock<IPeriodicalStore>();
            mockKindle.Setup(mock => mock.GetPeriodicals()).Returns(new List<FileInfo>());
            var sut = new SingleCleaner(mockKindle.Object);

            //act
            var result = sut.Clean();

            //assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.FilesCleaned.Count);
        }

        private List<FileInfo> makePobiList(params string[] filenames) {
            string fakePath = @"W:\testDirectoryShouldNotExist\";
            var result = new List<FileInfo>();
            foreach (string filename in filenames) result.Add(new FileInfo(string.Concat(fakePath, filename, ".pobi")));
            return result;
        }

        [TestMethod]
        public void Clean_RunWithUniquePrefixedFiles_Succeeds() {
            //arrange
            var mockKindle = new Mock<IPeriodicalStore>();
            mockKindle.Setup(mock => mock.GetPeriodicals())
                .Returns(makePobiList("TestPrefix1_Suffix1", "TestPrefix2_Suffix2", "TestPrefixRed_Suffix3", "TestPrefixBlue_Suffix4"));
            var sut = new SingleCleaner(mockKindle.Object);

            //act
            var result = sut.Clean();

            //assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.FilesCleaned.Count);
        }

        [TestMethod]
        public void Clean_RunWithSomeSamePrefixedFiles_Succeeds() {
            //arrange
            var mockKindle = new Mock<IPeriodicalStore>();
            mockKindle.Setup(mock => mock.GetPeriodicals())
                .Returns(makePobiList("TestPrefix1_Suffix1", "TestPrefix1_Suffix2", "TestPrefixGreen_Suffix3", "TestPrefixGreen_Suffix4", "TestPrefixYes_Suffix5"));
            var sut = new SingleCleaner(mockKindle.Object);

            //act
            var result = sut.Clean();

            //assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(2, result.FilesCleaned.Count);
        }
        [TestMethod]
        public void Clean_RunWithAllSamePrefixedFiles_Succeeds() {
            //arrange
            var mockKindle = new Mock<IPeriodicalStore>();
            mockKindle.Setup(mock => mock.GetPeriodicals())
                .Returns(makePobiList("TestPrefixGreen_Suffix1", "TestPrefixGreen_Suffix2", "TestPrefixGreen_Suffix3", "TestPrefixGreen_Suffix4", "TestPrefixGreen_Suffix5"));
            var sut = new SingleCleaner(mockKindle.Object);

            //act
            var result = sut.Clean();

            //assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(4, result.FilesCleaned.Count);
        }
        [TestMethod]
        public void Clean_RunWithSomeIdenticalPrefixedFilesInDifferentFolders_Succeeds() {
            //arrange
            var mockKindle = new Mock<IPeriodicalStore>();
            mockKindle.Setup(mock => mock.GetPeriodicals())
                .Returns(makePobiList(@"a\TestPrefix1_Suffix1", @"b\TestPrefix1_Suffix1", @"c\TestPrefix1_Suffix1", "TestPrefix1_Suffix1", "TestPrefixGreen_Suffix4", "TestPrefixYes_Suffix5"));
            var sut = new SingleCleaner(mockKindle.Object);

            //act
            var result = sut.Clean();

            //assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(3, result.FilesCleaned.Count);
        }

        [TestMethod]
        public void Clean_RunWithSomeIdenticalPrefixedFilesWithDotInFilenameInDifferentFolders_Succeeds() {
            //arrange
            var mockKindle = new Mock<IPeriodicalStore>();
            mockKindle.Setup(mock => mock.GetPeriodicals())
                .Returns(makePobiList(@"a\TestPrefix1_Suffix1.old", @"b\TestPrefix1_Suffix1.old", @"c\TestPrefix1_Suffix1", "TestPrefix1_Suffix1", "TestPrefixGreen_Suffix4", "TestPrefixYes_Suffix5"));
            var sut = new SingleCleaner(mockKindle.Object);

            //act
            var result = sut.Clean();

            //assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(3, result.FilesCleaned.Count);
        }

    }
}
