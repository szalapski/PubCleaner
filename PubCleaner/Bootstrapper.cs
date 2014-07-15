using System.Collections.Generic;
using System.IO;
using Szalapski.PubCleaner.Lib;

namespace Szalapski.PubCleaner.App {
    internal static class Bootstrapper {
        internal static MainWindowViewModel GetMainWindowViewModel() {
            return new MainWindowViewModel(new SingleCleaner(), new KindleDetector());   // TODO: how to dispose of kindledetector properly?
        }
    }
}
