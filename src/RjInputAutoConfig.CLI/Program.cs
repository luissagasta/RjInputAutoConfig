using static MessagesHelper;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            if (!TryGetGamepadsAndWriteRyujinxConfig())
            {
                return;
            }
            RunRyujinxIfNeeded(args);
        }
        catch (Exception ex)
        {
            ShowError(ex);
            Environment.Exit(-1);
        }
    }

    static void RunRyujinxIfNeeded(string[] args) => ProgramRunner.RunIfPathIsNotEmpty(Settings.Current.RyujinxProgramFilepath, args);

    static bool TryGetGamepadsAndWriteRyujinxConfig()
    {
        if (!TryGetGamepadsAndShowFoundGamepads(out var gamepadIds))
        {
            return false;
        }
        WriteRyujinxConfig(gamepadIds);
        return true;
    }

    static void WriteRyujinxConfig(List<string> gamepadIds) => WriteRyujinxConfig(Settings.Current.RyujinxConfigFilepath, gamepadIds);

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