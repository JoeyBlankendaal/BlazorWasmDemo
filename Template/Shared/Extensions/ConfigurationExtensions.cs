using Microsoft.Extensions.Configuration;

namespace Template.Shared.Extensions;

public static class ConfigurationExtensions
{
    public static string[] GetAppAreas(this IConfiguration config) => config.GetSection("App:Areas").Get<string[]>();
    public static string GetAppDatabase(this IConfiguration config) => config["App:Database"];
    public static string GetAppName(this IConfiguration config) => config["App:Name"];
    public static string GetAppUrl(this IConfiguration config) => config["App:Url"];

    public static string GetDatabaseConnectionString(this IConfiguration config, string database) =>
        config[$"Databases:{database}:ConnectionString"];

    public static string GetDatabaseDbms(this IConfiguration config, string database) =>
        config[$"Databases:{database}:Dbms"];

    public static bool GetDatabaseRecreateOnRun(this IConfiguration config, string database) =>
        config.GetValue<bool>($"Databases:{database}:RecreateOnRun");

    public static string GetSmtpHost(this IConfiguration config) => config["Smtp:Host"];
    public static string GetSmtpPassword(this IConfiguration config) => config["Smtp:Password"];
    public static int GetSmtpPort(this IConfiguration config) => int.Parse(config["Smtp:Port"]);
    public static string GetSmtpUserName(this IConfiguration config) => config["Smtp:UserName"];
}
