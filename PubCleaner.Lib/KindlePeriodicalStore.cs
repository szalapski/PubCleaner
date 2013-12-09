using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Szalapski.PubCleaner.Lib {

    /// <summary>
    /// Represents a Kindle's stored periodicals.
    /// </summary>
    public class KindlePeriodicalStore : IPeriodicalStore {

        public KindlePeriodicalStore(DirectoryInfo kindleRootDirectory) {
            if (kindleRootDirectory == null) throw new ArgumentNullException("kindleRootDirectory");
            root = kindleRootDirectory;
        }
        private DirectoryInfo root;

        public IEnumerable<FileInfo> GetPeriodicals() {
            return root.EnumerateFiles("*.pobi", SearchOption.AllDirectories);
        }

        public void Delete(System.IO.FileInfo periodical) {
            periodical.Delete();
        }

        public void Delete(IEnumerable<System.IO.FileInfo> periodicals) {
            foreach (FileInfo periodical in periodicals) Delete(periodical);
        }
    }
}
