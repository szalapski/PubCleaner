using System.Windows;
using Szalapski.PubCleaner.App;

namespace Szalapski.PubCleaner {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public MainWindow() {
            vm = Bootstrapper.GetMainWindowViewModel();
            this.DataContext = vm;
            InitializeComponent();
        }

        private IMainWindowViewModel vm;

    }
}
