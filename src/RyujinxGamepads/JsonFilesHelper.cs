using System.Text.Json;
using System.Text.Json.Nodes;
namespace RyujinxGamepads;

static class JsonFilesHelper
{
    internal static void WriteFile(JsonNode node, string configFilePath)
    {
        using var fs = File.Create(configFilePath);
        using var writer = new Utf8JsonWriter(fs, new JsonWriterOptions { Indented = true });
        node.WriteTo(writer);
    }

    internal static JsonNode GetJsonFileContentOrThrowException(string filePath, string fileDescription)
    {
        try
        {
            return GetJsonFileContent(filePath);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error reading {fileDescription} at '{filePath}': {ex.Message}", ex);
        }
    }

    static JsonNode GetJsonFileContent(string filePath) => JsonNode.Parse(GetFileContent(filePath));

    static string GetFileContent(string filePath) => File.ReadAllText(filePath);

}