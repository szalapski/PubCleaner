using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szalapski.PubCleaner.Lib {
    public class CleanResults {
        public CleanResults(bool success) {
            Success = success;
        }

        public bool Success { get; set; }
    }
}
