using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
namespace Szalapski.PubCleaner.App {
    public interface IMainWindowViewModel  {
        void CleanNow(DirectoryInfo directory);
        ObservableCollection<DirectoryInfo> Directories {get;set;}
        string AppConsole { get; set; }
    }
}
