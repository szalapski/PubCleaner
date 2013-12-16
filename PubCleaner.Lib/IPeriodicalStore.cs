using System.Collections.Generic;
using System.IO;

namespace Szalapski.PubCleaner.Lib {
    
    /// <summary>
    /// Implementations represent the files on an attached kindle and provide functions for working with them
    /// </summary>
    public interface IPeriodicalStore {
        IEnumerable<FileInfo> GetPeriodicals();
        void Delete(FileInfo periodical);
        void Delete(IEnumerable<FileInfo> periodicals);
    }
}
