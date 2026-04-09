using System;
using Microsoft.Extensions.Configuration;
namespace RyujinxGamepads;

internal class Settings
{
    static readonly string ConfigFilePath = $"appsettings.json";
    IConfiguration Configuration { get; } = new ConfigurationBuilder().AddJsonFile(ConfigFilePath).Build();
    T GetValueOrThrowException<T>(string key) where T : class => GetValue<T>(key) ?? throw new Exception($"Key '{key}' not found in configuration file '{ConfigFilePath}'.");
    T GetValue<T>(string key) where T : class => Configuration[key] as T;

    public string RyujinxConfigFilepath => Environment.ExpandEnvironmentVariables(GetValueOrThrowException<string>("RyujinxConfigFilepath"));

}