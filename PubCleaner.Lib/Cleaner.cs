using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Szalapski.PubCleaner.Lib {
    public class Cleaner {
        private IFileStore kindle;
        
        public Cleaner(IFileStore kindle) {
            this.kindle = kindle;
        }

        /// <summary>
        /// Cleans all old periodcials/publications from the attached Kindle, leaving only the most recent one
        /// </summary>
        public CleanResults Clean() {
            List<FileInfo> periodicals = kindle.GetPeriodicals();
            List<FileInfo> oldOnes = FilterToOldPeriodicals(periodicals);
            kindle.Delete(oldOnes);
            var result = new CleanResults(true);
            result.FilesCleaned.AddRange(oldOnes);
            return result;
        }

        private List<FileInfo> FilterToOldPeriodicals(IEnumerable<FileInfo> periodicals) {
            if (periodicals == null) throw new ArgumentNullException("periodicals");
            List<FileInfo> results = new List<FileInfo>();
            foreach (var file in periodicals.OrderBy( f => f.CreationTime)) {
                string frontOfFilename = file.Name.Substring(0, file.Name.LastIndexOf('_')+1);
                if (!results.Any(f => f.Name.StartsWith(frontOfFilename))) results.Add(file);
            }
            return results;
        }
    }
 }
