using System.IO;
using System.Collections.Generic;

namespace Szalapski.PubCleaner.Lib {
    public interface IKindleDetector {
        IEnumerable<DirectoryInfo> DetectKindleDirectories();
    }
}
