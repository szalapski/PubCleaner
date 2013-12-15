using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Szalapski.PubCleaner.Lib;
using WpfControls;

namespace Szalapski.PubCleaner {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            CleanNowButton.Click += new RoutedEventHandler((x, y) => this.CleanNow());  //TODO: binding
        }

        public string CleanResults { get; set; }

        private void CleanNow() {
            //TODO: make instancy
            IEnumerable<DirectoryInfo> directories = KindleDetector.DetectKindleDirectories();
                StringBuilder resultsBuilder = new StringBuilder();
            foreach (DirectoryInfo kindleDirectory in directories){
                var cleaner = new Cleaner(new KindlePeriodicalStore(kindleDirectory, pretendDelete:true));
                var results = cleaner.Clean();
                foreach (FileInfo file in results.FilesCleaned){
                    resultsBuilder.AppendLine(string.Format("Deleted {0}", file.FullName));
                }
            }
            CleanResults = resultsBuilder.ToString();
            ResultsTextBlock.Text = CleanResults; // todo: binding
            
        }
    }
}
