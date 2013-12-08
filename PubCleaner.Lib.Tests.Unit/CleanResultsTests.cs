using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Szalapski.PubCleaner.Lib.Tests.Unit {
    [TestClass]
    class CleanResultsTests {
        [TestMethod]
        public void Constructor_True_IsSetProperly() {
            var sut = new CleanResults(success: true);
            Assert.IsTrue(sut.Success);
        }
        [TestMethod]
        public void Constructor_False_IsSetProperly() {
            var sut = new CleanResults(success: false);
            Assert.IsFalse(sut.Success);
        }
        [TestMethod]
        public void Constructor_False_FilesCleanedIsNotNull() {
            var sut = new CleanResults(success: false);
            Assert.IsNotNull(sut.FilesCleaned);
        }
        [TestMethod]
        public void Constructor_False_FilesCleanedIsEmpty() {
            var sut = new CleanResults(success: false);
            Assert.AreEqual(0, sut.FilesCleaned.Count);
        }

    }
}
