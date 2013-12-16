using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubCleaner.TestsCommon {
    public static class FileHelpers {
        public static void deleteContents(DirectoryInfo tempDirectory) {
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

    }
}
