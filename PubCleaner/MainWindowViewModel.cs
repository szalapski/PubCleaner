using System.IO;
using System.Text;
using Szalapski.PubCleaner.Lib;

namespace Szalapski.PubCleaner.App {
    public class MainWindowViewModel : NotifiableBase, IMainWindowViewModel{
        public MainWindowViewModel(ICleaner cleaner) {
            this.cleaner = cleaner;
        }
        private ICleaner cleaner;

        public string CleanResults {
            get { return _cleanResults;}
            set{ 
                _cleanResults = value;
                NotifyPropertyChanged();
            }
        }
        private string _cleanResults;
       
        public void CleanNow() {
            CleanResults results = cleaner.Clean();
            var resultsBuilder = new StringBuilder();
            foreach (FileInfo file in results.FilesCleaned) {
                resultsBuilder.AppendLine(string.Format("Deleted {0}", file.FullName));
            }
            resultsBuilder.AppendLine(string.Format("{0} periodical{1} deleted", results.FilesCleaned.Count, results.FilesCleaned.Count==1 ? "" : "s"));
            CleanResults += resultsBuilder.ToString();
        }
        
    }
}
