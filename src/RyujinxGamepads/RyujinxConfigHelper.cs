using System.Text.Json;
using System.Text.Json.Nodes;
namespace RyujinxGamepads;

class RyujinxConfigHelper
{
    internal static void ReplaceInputConfig(string configFilePath, IEnumerable<string> gamepadIds)
    {
        var config = GetConfigOrThrowException(configFilePath);
        ReplaceInputConfig(config, gamepadIds);
        WriteConfigFile(config, configFilePath);
    }

    static JsonNode GetConfigOrThrowException(string configFilePath)
    {
        try
        {
            return GetConfig(configFilePath);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error reading Ryujinx config file at '{configFilePath}': {ex.Message}", ex);
        }
    }

    static JsonNode GetConfig(string configFilePath) => JsonNode.Parse(GetConfigFileContent(configFilePath));

    static void ReplaceInputConfig(JsonNode config, IEnumerable<string> gamepadIds)
    {
        config["input_config"] = GetInputConfig(gamepadIds);
    }

    static string GetConfigFileContent(string configFilePath) => File.ReadAllText(configFilePath);

    static void WriteConfigFile(JsonNode node, string configFilePath)
    {
        using var fs = File.Create(configFilePath);
        using var writer = new Utf8JsonWriter(fs, new JsonWriterOptions { Indented = true });
        node.WriteTo(writer);
    }

    static JsonArray GetInputConfig(IEnumerable<string> gamepadIds) => [.. gamepadIds.Select((gamepadId, index) => GetSingleInputConfig(gamepadId, index + 1))];

    static JsonNode GetSingleInputConfig(string gamepadId, int playerIndex)
    {
        var node = GetDesiredSingleInputSectionOrThrowException();
        node["id"] = gamepadId;
        node["player_index"] = $"Player{playerIndex}";
        return node;
    }

    static JsonNode GetDesiredSingleInputSectionOrThrowException()
    {
        try
        {
            return GetDesiredSingleInputSection();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error parsing desired input config section: {ex.Message}", ex);
        }
    }

    static JsonNode GetDesiredSingleInputSection() => JsonNode.Parse(JsonSingleInputSection);

    static readonly string JsonSingleInputSection = "{\n      \"left_joycon_stick\": {\n        \"joystick\": \"Left\",\n        \"invert_stick_x\": false,\n        \"invert_stick_y\": false,\n        \"rotate90_cw\": false,\n        \"stick_button\": \"LeftStick\"\n      },\n      \"right_joycon_stick\": {\n        \"joystick\": \"Right\",\n        \"invert_stick_x\": false,\n        \"invert_stick_y\": false,\n        \"rotate90_cw\": false,\n        \"stick_button\": \"RightStick\"\n      },\n      \"deadzone_left\": 0.1,\n      \"deadzone_right\": 0.1,\n      \"range_left\": 1,\n      \"range_right\": 1,\n      \"trigger_threshold\": 0.5,\n      \"motion\": {\n        \"motion_backend\": \"GamepadDriver\",\n        \"sensitivity\": 100,\n        \"gyro_deadzone\": 1,\n        \"enable_motion\": false\n      },\n      \"rumble\": {\n        \"strong_rumble\": 1,\n        \"weak_rumble\": 1,\n        \"enable_rumble\": true\n      },\n      \"left_joycon\": {\n        \"button_minus\": \"Back\",\n        \"button_l\": \"LeftShoulder\",\n        \"button_zl\": \"LeftTrigger\",\n        \"button_sl\": \"Unbound\",\n        \"button_sr\": \"Unbound\",\n        \"dpad_up\": \"DpadUp\",\n        \"dpad_down\": \"DpadDown\",\n        \"dpad_left\": \"DpadLeft\",\n        \"dpad_right\": \"DpadRight\"\n      },\n      \"right_joycon\": {\n        \"button_plus\": \"Start\",\n        \"button_r\": \"RightShoulder\",\n        \"button_zr\": \"RightTrigger\",\n        \"button_sl\": \"Unbound\",\n        \"button_sr\": \"Unbound\",\n        \"button_x\": \"X\",\n        \"button_b\": \"B\",\n        \"button_y\": \"Y\",\n        \"button_a\": \"A\"\n      },\n      \"version\": 1,\n      \"backend\": \"GamepadSDL2\",\n      \"id\": \"\",\n      \"controller_type\": \"ProController\",\n      \"player_index\": \"\"\n    }";

}