using System.ComponentModel;
namespace Szalapski.PubCleaner.App {
    public interface IMainWindowViewModel  {
        void CleanNow();
        string CleanResults { get; set; }
    }
}
