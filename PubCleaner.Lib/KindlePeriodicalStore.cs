using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Szalapski.PubCleaner.Lib {

    /// <summary>
    /// Represents a Kindle's stored periodicals.
    /// </summary>
    public class KindlePeriodicalStore : IPeriodicalStore {

        public KindlePeriodicalStore(DirectoryInfo kindleDocumentsDirectory, bool pretendDelete = false) {
            if (kindleDocumentsDirectory == null) throw new ArgumentNullException("kindleDocumentsDirectory");
            root = kindleDocumentsDirectory;
            deleteEnabled = !pretendDelete;
        }
        private DirectoryInfo root;
        private bool deleteEnabled;

        public IEnumerable<FileInfo> GetPeriodicals() {
            return root.EnumerateFiles("*.pobi", SearchOption.AllDirectories);
        }

        public void Delete(System.IO.FileInfo periodical) {
            if (periodical.Extension == ".pobi") {
                IEnumerable<FileInfo> relatedFiles = periodical.Directory
                    .EnumerateFiles(string.Format("{0}.*", GetNameWithoutExtension(periodical)), SearchOption.AllDirectories)
                    .Where(file => file.Extension != ".pobi");
                foreach (FileInfo relatedFile in relatedFiles) {
                    if (deleteEnabled) relatedFile.Delete();
                    System.Console.WriteLine("  Delete related file {0}", relatedFile.FullName);
                }
   
                IEnumerable<DirectoryInfo> relatedDirectories = periodical.Directory
                    .EnumerateDirectories(string.Format("{0}.*", GetNameWithoutExtension(periodical)), SearchOption.AllDirectories)
                    .Where(dir => dir.Extension != ".pobi");
                foreach (DirectoryInfo relatedDirectory in relatedDirectories) {
                    if (deleteEnabled) relatedDirectory.Delete();
                    System.Console.WriteLine("  Delete related directory {0}", relatedDirectory.FullName);
                }
            }
            if (deleteEnabled) periodical.Delete();
            System.Console.WriteLine("Delete {0}", periodical.FullName);
        }

        public void Delete(IEnumerable<System.IO.FileInfo> periodicals) {
            foreach (FileInfo periodical in periodicals) Delete(periodical);
        }

        private static string GetNameWithoutExtension(FileInfo file) {
            if (file == null) throw new ArgumentNullException("file");
            int indexOfExtension = file.Name.LastIndexOf(file.Extension);
            if (indexOfExtension > 0) return file.Name.Remove(indexOfExtension);
            return file.Name;
        }
    }
}
