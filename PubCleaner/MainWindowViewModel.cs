using System.IO;
using System.Text;
using Szalapski.PubCleaner.Lib;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Windows;
using System.Threading.Tasks;

namespace Szalapski.PubCleaner.App {
    public class MainWindowViewModel : NotifiableBase, IMainWindowViewModel {
        public MainWindowViewModel(ICleaner cleaner, IPeriodicalDirectoryDetector detector) {
            _cleaner = cleaner;
            _detector = detector;
            detector.DirectoriesChanged += new DirectoriesChangedEventHander(HandleDirectoriesChanged);
            Task.Factory.StartNew(() => detector.Detect()) // Needed because the first event fired before we even got to this class.  TODO: how to get rid of?
                .ContinueWith((task) => Reload(task.Result));
        }
        private ICleaner _cleaner;
        private IPeriodicalDirectoryDetector _detector;

        #region view state
        private ObservableCollection<DirectoryInfo> _directories = new ObservableCollection<DirectoryInfo>();
        public ObservableCollection<DirectoryInfo> Directories {
            get { return _directories; }
            set {
                _directories = value;
                NotifyPropertyChanged();
            }
        }

        public string AppConsole {
            get { return _appConsole; }
            set {
                _appConsole = value;
                NotifyPropertyChanged();
            }
        }
        private string _appConsole;

        #endregion


        public void CleanNow(DirectoryInfo directory) {
            CleanResults results = _cleaner.Clean(directory);
            AppConsole += results;
        }

        private void HandleDirectoriesChanged(object sender, DirectoriesChangedEventArgs e) {
            Application.Current.Dispatcher.Invoke(() => {
                Reload(e.Directories);
            });
        }

        private void Reload(IEnumerable<DirectoryInfo> newDirectories) {
            Directories.Clear();
            foreach (DirectoryInfo d in newDirectories) Directories.Add(d);
        }

    }
}
