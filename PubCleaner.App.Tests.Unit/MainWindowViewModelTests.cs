using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Szalapski.PubCleaner.App;
using System.Collections.Generic;
using Szalapski.PubCleaner.Lib;
using Moq;
using System.IO;

namespace PubCleaner.App.Tests.Unit {
    [TestClass]
    public class MainWindowViewModelTests {
        [TestMethod]
        public void CleanNow_NoStores_NoResults() {
            Assert.Inconclusive();
        }
        [TestMethod]
        public void CleanNow_NormalCall_UpdatesCleanResults() {
            // TODO: looks like FileInfo objects have to correspond to existing files
            //var fakeCleanResults = new CleanResults(success:true);
            //fakeCleanResults.FilesCleaned.Add(new FileInfo("foo.txt"){CreationTime = DateTime.Now.AddDays(-1)});
            //fakeCleanResults.FilesCleaned.Add(new FileInfo("fake1_s1.azw"){CreationTime = DateTime.Now.AddDays(-1)});
            //fakeCleanResults.FilesCleaned.Add(new FileInfo("fake1_s2.pobi"){CreationTime = DateTime.Now.AddDays(-1)});
            //fakeCleanResults.FilesCleaned.Add(new FileInfo("fake1_s3.pobi"){CreationTime = DateTime.Now});
            //fakeCleanResults.FilesCleaned.Add(new FileInfo("fake2_s4.pobi"){CreationTime = DateTime.Now});
            
            //var mockCleaner = new Mock<ICleaner>();
            //mockCleaner.Setup(c => c.Clean()).Returns(fakeCleanResults);
            //var x = mockCleaner.Object;
            //Console.Write(x);
            //var sut = new MainWindowViewModel(mockCleaner.Object);
            //sut.CleanNow();
            //Assert.IsTrue(sut.CleanResults.Contains("fake1_s1.pobi"));
            //Assert.IsTrue(sut.CleanResults.Contains("fake1_s1.azw"));
            //Assert.IsTrue(sut.CleanResults.Contains("fake1_s2.pobi"));
            Assert.Inconclusive();
        }
    }
}
