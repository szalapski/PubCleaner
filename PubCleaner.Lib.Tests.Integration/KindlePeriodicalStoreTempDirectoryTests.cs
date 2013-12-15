using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Szalapski.PubCleaner.Lib;

namespace PubCleaner.Lib.Tests.Integration {
    [TestClass]
    class KindlePeriodicalStoreTempDirectoryTests {
        private static string testPath = Path.Combine(Path.GetTempPath(), "Pubcleaner.Lib.Tests.Integration");
        private static DirectoryInfo testDirectory = new DirectoryInfo(testPath);


        [ClassInitialize()]
        public static void ClassInit(TestContext context) {
            testDirectory.Create();
            deleteContents(testDirectory);
        }

        [ClassCleanup]
        public static void ClassCleanup() {
            deleteContents(testDirectory);
        }

        private static void deleteContents(DirectoryInfo tempDirectory) {
            foreach (FileInfo file in tempDirectory.GetFiles()) {
                try {
                    file.Delete();
                }
                catch (IOException) { }
            }
            foreach (DirectoryInfo directory in tempDirectory.GetDirectories()) {
                try {
                    directory.Delete(recursive: true);
                }
                catch (IOException) { }
            }
        }

        private void createPobiFiles(string subdirectory, params string[] filenames) {
            if (string.IsNullOrWhiteSpace(subdirectory)) subdirectory = string.Empty;
            string parentDirectory = Path.Combine(testDirectory.FullName, subdirectory);
            Directory.CreateDirectory(parentDirectory);
            foreach (string filename in filenames) {
                File.Create(Path.Combine(parentDirectory, filename + ".pobi"));
            }
        }

        private string[] testFilenames1 = { "orange_suffix1", "orange_suffix2", "orange_suffix3", "green_suffix4", "green_suffix5", "purple_suffix6" };

        [TestMethod]
        public void GetPeriodicals_SixFilesInRoot_GetsRightFiles() {
            string[] filenames = testFilenames1;
            createPobiFiles(null, filenames);
            var store = new KindlePeriodicalStore(testDirectory);
            IEnumerable<FileInfo> files = store.GetPeriodicals();
            Assert.AreEqual(filenames.Count(), files.Count());
            foreach (var testName in filenames) {
                Assert.IsTrue(files.Any(f => f.Name.Contains(testName)), string.Format("Couldn't find {0}", testName));
            }
        }
        [TestMethod]
        public void GetPeriodicals_SixFilesInFolder_GetsRightFiles() {
            string[] filenames = testFilenames1;
            createPobiFiles(@"pub1", filenames);
            var store = new KindlePeriodicalStore(new DirectoryInfo(Path.Combine(testPath, "pub1")));
            IEnumerable<FileInfo> files = store.GetPeriodicals();
            Assert.AreEqual(filenames.Count(), files.Count());
            Assert.IsTrue(files.All(f => f.FullName.Contains("pub1")));
            foreach (var testName in filenames) {
                Assert.IsTrue(files.Any(f => f.Name.Contains(testName)), string.Format("Couldn't find {0}", testName));
            }
        }
        [TestMethod]
        public void GetPeriodicals_TwelveFilesInTwoFolders_GetsRightFiles() {
            string[] filenames = testFilenames1;
            createPobiFiles(@"pub2\sub1", filenames);
            createPobiFiles(@"pub2\sub2", filenames);
            var store = new KindlePeriodicalStore(new DirectoryInfo(Path.Combine(testPath, "pub2")));
            IEnumerable<FileInfo> files = store.GetPeriodicals();
            Assert.AreEqual(filenames.Count() * 2, files.Count());
            foreach (var testName in filenames) {
                Assert.IsTrue(files.All(f => f.FullName.Contains("pub2")));
                Assert.IsTrue(files.Any(f => f.Name.Contains(testName) && f.DirectoryName.Contains("sub1")), string.Format(@"Couldn't find sub1\{0}", testName));
                Assert.IsTrue(files.Any(f => f.Name.Contains(testName) && f.DirectoryName.Contains("sub2")), string.Format(@"Couldn't find sub2\{0}", testName));
            }
        }

        [TestMethod]
        public void GetPeriodicalsEighteenFilesInNestedFolders_GetsRightFiles() {
            string[] filenames = testFilenames1;
            createPobiFiles(@"pub3", filenames);
            createPobiFiles(@"pub3\sub1", filenames);
            createPobiFiles(@"pub3\sub1\sub1a", filenames);
            var store = new KindlePeriodicalStore(new DirectoryInfo(Path.Combine(testPath, "pub3")));
            IEnumerable<FileInfo> files = store.GetPeriodicals();
            Assert.AreEqual(filenames.Count() * 3, files.Count());
            foreach (var testName in filenames) {
                Assert.IsTrue(files.All(f => f.FullName.Contains("pub3")));
                Assert.IsTrue(files.Any(f => f.Name.Contains(testName) && f.DirectoryName.Contains(@"pub3\sub1")), string.Format(@"Couldn't find sub1\{0}", testName));
                Assert.IsTrue(files.Any(f => f.Name.Contains(testName) && f.DirectoryName.Contains(@"pub3\sub1\sub1a")), string.Format(@"Couldn't find sub1\sub1a\{0}", testName));
            }
        }
    }
}
