using RyujinxGamepads;
using static RyujinxGamepads.MessagesHelper;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            GetGamepadsAndWriteRyujinxConfig();
        }
        catch (Exception ex)
        {
            ShowError(ex);
            Environment.Exit(-1);
        }
    }

    static void GetGamepadsAndWriteRyujinxConfig()
    {
        if (!TryGetGamepadsAndShowFoundGamepads(out var gamepadIds))
        {
            return;
        }
        WriteRyujinxConfig(gamepadIds);
    }

    static void WriteRyujinxConfig(List<string> gamepadIds) => WriteRyujinxConfig(new Settings().RyujinxConfigFilepath, gamepadIds);

    static void WriteRyujinxConfig(string ryujinxConfigFilepath, List<string> gamepadIds)
    {
        RyujinxConfigHelper.ReplaceInputConfig(ryujinxConfigFilepath, gamepadIds);
        ShowSuccess($"Config file written to {ryujinxConfigFilepath}");
    }

    static bool TryGetGamepadsAndShowFoundGamepads(out List<string> gamepadIds)
    {
        gamepadIds = GamepadsHelper.GetGamepadRiujynxIds();
        if (gamepadIds.Count == 0)
        {
            ShowWarning("No gamepads found.");
            return false;
        }
        WriteFoundGamepads(gamepadIds);
        return true;
    }

    static void WriteFoundGamepads(List<string> gamepadIds) => Show($"Gamepads found: {gamepadIds.Count}{Environment.NewLine}{gamepadIds.Aggregate((previous, current) => $"{previous}{Environment.NewLine}{current}")}");
}