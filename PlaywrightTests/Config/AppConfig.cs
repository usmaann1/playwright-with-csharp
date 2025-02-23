using System;
using System.IO;
using Newtonsoft.Json;

public static class AppConfig
{
    public static string? AppUrl { get; private set; }
    public static string? Email { get; private set; }
    public static string? Password { get; private set; }

    static AppConfig()
    {
        LoadConfiguration();
    }

    private static void LoadConfiguration()
    {
        string projectRoot = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
        string configPath = Path.Combine(projectRoot, "Config", "env.json");

        if (!File.Exists(configPath))
            throw new FileNotFoundException($"Configuration file not found at: {configPath}");

        var configText = File.ReadAllText(configPath);
        dynamic config = JsonConvert.DeserializeObject<dynamic>(configText) ?? throw new InvalidOperationException("Configuration file is empty or invalid.");

        AppUrl = config.AppUrl;
        Email = config.Email;
        Password = config.Password;
    }
}
