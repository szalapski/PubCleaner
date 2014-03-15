using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Szalapski.PubCleaner.Lib {
    public class MultiCleaner : ICleaner {
        public MultiCleaner(IPeriodicalDirectoryDetector directoryDetector) {
            this.directoryDetector = directoryDetector;
        }
        private IPeriodicalDirectoryDetector directoryDetector;
       
        public bool FakeIt { get { return fakeIt; } set { fakeIt = value; } } 
        private bool fakeIt = false;

        public CleanResults Clean() {
            IEnumerable<DirectoryInfo> directories = directoryDetector.Detect();

            IEnumerable<IPeriodicalStore> stores = PeriodicalStoreFactory.MakeStores(directories, FakeIt);  // hard dependency
            StringBuilder resultsBuilder = new StringBuilder();
            var results = new CleanResults(true);
            foreach (IPeriodicalStore store in stores) {
                CleanResults partialResult = new SingleCleaner(store).Clean();
                results.Success &= partialResult.Success;
                results.FilesCleaned.AddRange(partialResult.FilesCleaned);
            }
            return results;
        }
    }
}
