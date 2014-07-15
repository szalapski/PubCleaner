using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System;


namespace Szalapski.PubCleaner.Lib {

    public delegate void DirectoriesChangedEventHander(object sender, DirectoriesChangedEventArgs e);

    public class KindleDetector : IPeriodicalDirectoryDetector, IDisposable {
        public KindleDetector() {
            SetUpAttachmentDetection();
            OnDirectoriesChanged();
        }

        private ManagementEventWatcher watcher = null;
        private ManagementEventWatcher watcher2 = null;

        /// <summary>
        /// Raised when directories have changed.
        /// </summary>
        public event DirectoriesChangedEventHander DirectoriesChanged;
        // Invoke the Changed event; called whenever list changes
        protected virtual void OnDirectoriesChanged() {
            if (DirectoriesChanged != null) DirectoriesChanged(this, new DirectoriesChangedEventArgs() { Directories = Detect() });
        }

        public IEnumerable<DirectoryInfo> Detect() {
            var files = Directory.GetLogicalDrives();
            string[] driveBlacklist = { @"A:\", @"B:\", @"C:\" };
            IEnumerable<DriveInfo> drives = DriveInfo.GetDrives().Where(d => !driveBlacklist.Contains(d.Name));
            IEnumerable<DriveInfo> kindleDrives = drives.Where(d => d.IsReady && d.VolumeLabel == "Kindle");
            foreach (DriveInfo kindleDrive in kindleDrives) {
                yield return new DirectoryInfo(Path.Combine(kindleDrive.Name, "documents"));
            }
        }

        private void SetUpAttachmentDetection() {
            WqlEventQuery q, q2;
            ManagementOperationObserver observer = new ManagementOperationObserver();

            // Bind to local machine
            ConnectionOptions opt = new ConnectionOptions() { EnablePrivileges = true };
            ManagementScope scope = new ManagementScope("root\\CIMV2", opt);
            string wmiKindleQueryClause = @"TargetInstance ISA 'Win32_LogicalDisk' and TargetInstance.DriveType = 2 and TargetInstance.VolumeName = 'Kindle'";
            q = new WqlEventQuery("__InstanceCreationEvent", new TimeSpan(0, 0, 5), wmiKindleQueryClause);
            q2 = new WqlEventQuery("__InstanceDeletionEvent", new TimeSpan(0, 0, 5), wmiKindleQueryClause);
            watcher = new ManagementEventWatcher(scope, q);
            watcher2 = new ManagementEventWatcher(scope, q2);
            // register async. event handler
            watcher.EventArrived += new EventArrivedEventHandler(driveAttachEvent);
            watcher2.EventArrived += new EventArrivedEventHandler(driveAttachEvent);
            watcher.Start();
            watcher2.Start();
        }

        private void driveAttachEvent(object sender, EventArrivedEventArgs e) {
            PropertyData pd = e.NewEvent.Properties["TargetInstance"];
            if (pd != null) OnDirectoriesChanged();
        }


        public void Dispose() {
            if (watcher != null) watcher.Dispose();
            if (watcher2 != null) watcher2.Dispose();
        }
    }

    public class DirectoriesChangedEventArgs : EventArgs {
        public IEnumerable<DirectoryInfo> Directories { get; set; }
    }
}
