using Microsoft.Extensions.Configuration;
namespace RyujinxGamepads;

internal class Settings
{
    internal static Settings Current { get; } = new Settings();
    private Settings()
    {
        RyujinxConfigFilepath = GetPathOrThrowException("RyujinxConfigFilepath");
        RyujinxProgramFilepath = GetPath("RyujinxProgramFilepath");
    }
    readonly string ConfigFilePath = $"appsettings.json";
    IConfiguration Configuration { get; } = new ConfigurationBuilder().AddJsonFile(ConfigFilePath).Build();
    string GetPathOrThrowException(string key) => GetPath(key) ?? throw NewKeyNotFoundException(key);
    string GetPath(string key) => ExpandPath(GetValue<string>(key));
    string ExpandPath(string path) => string.IsNullOrWhiteSpace(path) ? path : Environment.ExpandEnvironmentVariables(path);
    T GetValue<T>(string key) where T : class => Configuration[key] as T;
    Exception NewKeyNotFoundException(string key) => new($"Key '{key}' not found in configuration file '{ConfigFilePath}'.");
    
    public string RyujinxConfigFilepath { get; }
    public string RyujinxProgramFilepath { get; }
}