using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Szalapski.PubCleaner.Lib;
using System.IO;

namespace PubCleaner.Lib.Tests.Integration {
    [TestClass]
    public class CleanerLiveKindleTests {

        [TestMethod]
        public void Clean_AgainstLiveKindle_FakeDeleteAndReport() {
            foreach (DirectoryInfo kindleDirectory in new KindleDetector().DetectKindleDirectories()) {
                Console.WriteLine("Detected kindle dir {0}", kindleDirectory.FullName);
                IPeriodicalStore store = new KindlePeriodicalStore(kindleDirectory, pretendDelete: true);
                SingleCleaner sut = new SingleCleaner(store);
                sut.Clean();
            }
            Assert.Inconclusive();
        }

    }
}
