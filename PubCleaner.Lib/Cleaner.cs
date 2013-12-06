namespace Szalapski.PubCleaner.Lib {
    public class Cleaner {
        
        /// <summary>
        /// Cleans all old periodcials/publications from the attached Kindle, leaving only the most recent one
        /// </summary>
        public CleanResults Clean() {
            return new CleanResults(true);
        }
    }
 }
