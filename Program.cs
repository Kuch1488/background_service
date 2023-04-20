using background_service;

var configurationService = new ConfigurationService();
var appSettings = configurationService.GetAppSettings();

if (string.IsNullOrWhiteSpace(appSettings.FolderToWatch))
{
    Console.WriteLine("Folder to watch is not specified in the configuration file.");
    return;
}

var folderToWatch = appSettings.FolderToWatch.Replace("\\", Path.DirectorySeparatorChar.ToString());
var folderWatcherService = new FolderWatcherService(folderToWatch);
folderWatcherService.StartWatching();

Console.WriteLine("Press any key to exit...");
Console.ReadKey();
