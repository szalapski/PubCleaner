using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Szalapski.PubCleaner.Lib;
using System.IO;

namespace PubCleaner.Lib.Tests.Integration {
    [TestClass]
    public class CleanerLiveKindleTests {

        [TestMethod]
        public void Clean_AgainstLiveKindle_FakeDeleteAndReport() {
            MultiCleaner sut = new MultiCleaner(new KindleDetector()) { Pretend = true };
            sut.Clean();
        }

    }
}
