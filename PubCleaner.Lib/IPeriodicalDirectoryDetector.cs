using System.IO;
using System.Collections.Generic;

namespace Szalapski.PubCleaner.Lib {
    public interface IPeriodicalDirectoryDetector {
        IEnumerable<DirectoryInfo> Detect();
        event DirectoriesChangedEventHander DirectoriesChanged;
    }
}
