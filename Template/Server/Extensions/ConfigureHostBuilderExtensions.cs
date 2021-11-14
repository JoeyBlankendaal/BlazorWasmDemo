using System.Reflection;

namespace Template.Server.Extensions;

public static class ConfigureHostBuilderExtensions
{
    public static void AddConfiguration(this ConfigureHostBuilder builder, string path)
    {
        // Get Server project path
        var serverPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Split(@"bin\")[0];

        // Get Shared and Client appsettings.json paths
        var sharedPath = Path.Combine(serverPath.Replace("Server", "Shared"), path);
        var clientPath = Path.Combine(serverPath.Replace("Server", "Client"), "wwwroot", "appsettings.json");

        if (!File.Exists(sharedPath))
        {
            throw new FileNotFoundException($"Shared appsettings.json '{sharedPath}' could not be found.");
        }

        if (!File.Exists(clientPath))
        {
            throw new FileNotFoundException($"Client appsettings.json '{clientPath}' could not be found.");
        }

        // Import Shared appsettings.json into Server appsettings.json
        builder.ConfigureAppConfiguration((hostBuilder, config) =>
        {
            config.AddJsonFile(sharedPath, optional: false, reloadOnChange: true);
        });

        // Import Shared appsettings.json into Client appsettings.json
        File.Copy(sharedPath, clientPath, overwrite: true);
    }
}
