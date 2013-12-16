using System.Collections.Generic;
using System.IO;
using Szalapski.PubCleaner.Lib;

namespace Szalapski.PubCleaner.App {
    internal static class Bootstrapper {
        internal static MainWindowViewModel GetMainWindowViewModel() {
            IEnumerable<DirectoryInfo> directories = new KindleDetector().DetectKindleDirectories();
            var stores = new List<IPeriodicalStore>();
            foreach (DirectoryInfo kindleDirectory in directories) stores.Add(new KindlePeriodicalStore(kindleDirectory, pretendDelete:false));
            return new MainWindowViewModel(new MultiCleaner(stores));
        }
    }
}
