using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szalapski.PubCleaner.Lib {
    /// <summary>
    /// Provides methods to make periodical stores from directories.
    /// </summary>
    /// 
    public static class PeriodicalStoreFactory {
        //TODO:  How to make this obey good DI?
        public static IEnumerable<IPeriodicalStore> MakeStores(IEnumerable<DirectoryInfo> directories, bool pretendDelete = false) { 
            return directories.Select(d => new KindlePeriodicalStore(d, pretendDelete));  //todo: handle non-kindle folders better
        }
    }
}
