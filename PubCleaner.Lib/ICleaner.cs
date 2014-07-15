using System;
using System.IO;

namespace Szalapski.PubCleaner.Lib {
    public interface ICleaner {
       CleanResults Clean(DirectoryInfo directory);
    }
}
