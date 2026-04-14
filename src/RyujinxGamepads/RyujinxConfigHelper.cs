using System.Text.Json.Nodes;
using static RyujinxGamepads.JsonFilesHelper;
namespace RyujinxGamepads;

class RyujinxConfigHelper
{
    internal static void ReplaceInputConfig(string configFilePath, IEnumerable<string> gamepadIds)
    {
        var config = GetConfigOrThrowException(configFilePath);
        ReplaceInputConfig(config, gamepadIds);
        WriteFile(config, configFilePath);
    }

    static JsonNode GetConfigOrThrowException(string configFilePath) => GetJsonFileContentOrThrowException(configFilePath, "Ryujinx config file");

    static void ReplaceInputConfig(JsonNode config, IEnumerable<string> gamepadIds)
    {
        config["input_config"] = GetInputConfig(gamepadIds);
    }

    static JsonArray GetInputConfig(IEnumerable<string> gamepadIds) => [.. gamepadIds.Select((gamepadId, index) => GetSingleInputConfig(gamepadId, index + 1))];

    static JsonNode GetSingleInputConfig(string gamepadId, int playerIndex)
    {
        var node = GetDesiredSingleInputSectionOrThrowException();
        node["id"] = gamepadId;
        node["player_index"] = $"Player{playerIndex}";
        return node;
    }

    static JsonNode GetDesiredSingleInputSectionOrThrowException() => GetJsonFileContentOrThrowException("ryujinxgamepadinput.json", "Ryujinx single input config file");

}