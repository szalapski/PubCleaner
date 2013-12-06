using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Szalapski.PubCleaner.Lib {
    
    /// <summary>
    /// Represents the results of a clean operation.
    /// </summary>
    public class CleanResults {
        public CleanResults(bool success) {
            Success = success;
            _filesCleaned = new List<FileInfo>();
        }

        public bool Success { get; set; }

        public List<FileInfo> FilesCleaned {
            get { return _filesCleaned; }
        }
        private List<FileInfo> _filesCleaned;
    }
}
