using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Szalapski.PubCleaner.Lib {
    public class KindleDetector : IKindleDetector {

        public IEnumerable<DirectoryInfo> DetectKindleDirectories() {
            var files = Directory.GetLogicalDrives();
            string[] driveBlacklist = { @"A:\", @"B:\", @"C:\" };
            IEnumerable<DriveInfo> drives = DriveInfo.GetDrives().Where(d => !driveBlacklist.Contains(d.Name));
            IEnumerable<DriveInfo> kindleDrives = drives.Where(d => d.IsReady && d.VolumeLabel == "Kindle");
            foreach (DriveInfo kindleDrive in kindleDrives) {
                yield return new DirectoryInfo(Path.Combine(kindleDrive.Name, "documents"));
            }
        }

    }
}
