using background_service;
using Microsoft.Extensions.Logging;

var configurationService = new ConfigurationService();
var appSettings = configurationService.GetAppSettings();

ILoggerFactory loggerFactory = LoggerFactory.Create(
    builder =>
    {
        builder.AddConsole();
        builder.AddDebug();
    });

ILogger<FolderWatcherService> logger = loggerFactory.CreateLogger<FolderWatcherService>();

if (string.IsNullOrWhiteSpace(appSettings.FolderToWatch))
{
    Console.WriteLine("Folder to watch is not specified in the configuration file.");
    return;
}

var folderToWatch = appSettings.FolderToWatch.Replace("\\", Path.DirectorySeparatorChar.ToString());
var watchPeriod = appSettings.WatchPeriod;
var folderWatcherService = new FolderWatcherService(folderToWatch, watchPeriod, logger);
folderWatcherService.StartWatching();

Console.WriteLine("Press any key to exit...");
Console.ReadKey();
