using System.Collections.Generic;
using System.Text;

namespace Szalapski.PubCleaner.Lib {
    public class MultiCleaner : ICleaner {
        public MultiCleaner(IEnumerable<IPeriodicalStore> stores) {
            this.stores = stores;
        }
        private IEnumerable<IPeriodicalStore> stores;

        public CleanResults Clean() {
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
