namespace background_service
{
    public class FolderWatcherService
    {
        private readonly string _folderToWatch;

        public FolderWatcherService(string folderToWatch)
        {
            _folderToWatch = folderToWatch;
        }

        public void StartWatching()
        {
            if(!Directory.Exists(_folderToWatch))
            {
                Console.WriteLine($"Folder {_folderToWatch} does not exist.");
                return;
            }

            Console.WriteLine($"Watching folder {_folderToWatch}...");

            using var watcher = new FileSystemWatcher(_folderToWatch);
            watcher.IncludeSubdirectories = true;
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite |NotifyFilters.DirectoryName;

            watcher.Created += OnChanged;
            watcher.Changed += OnChanged;
            watcher.Deleted += OnChanged;
            watcher.Renamed += OnRenamed;

            watcher.EnableRaisingEvents = true;

            Console.WriteLine("Press 'q' to quit the program.");
            while (Console.Read() != 'q') ;
        }

        private void OnRenamed(object source, RenamedEventArgs e)
        {
            Console.WriteLine($"{e.ChangeType} {e.OldFullPath} -> {e.FullPath}");
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            Console.WriteLine($"{e.ChangeType} {e.FullPath}");
        }
    }
}
