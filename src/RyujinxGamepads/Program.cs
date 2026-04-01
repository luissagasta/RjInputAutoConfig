using RyujinxGamepads;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            GetGamepadsAndWriteConfig();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static void GetGamepadsAndWriteConfig()
    {
        if(!TryGetGamepadsAndShowFoundGamepads(out var gamepadIds))
        {
            return;
        }
        WriteConfig(gamepadIds);
    }

    static void WriteConfig(List<string> gamepadIds)
    {
        var ryujinxConfigFilepath = @"D:\Switch\Ryujinx\portable\Config-ps.json";
        RyujinxConfigHelper.ReplaceInputConfig(ryujinxConfigFilepath, gamepadIds);
        Console.WriteLine($"Config file written to {ryujinxConfigFilepath}");
    }

    static bool TryGetGamepadsAndShowFoundGamepads(out List<string> gamepadIds)
    {
        gamepadIds = GamepadsHelper.GetGamepadRiujynxIds();
        if (gamepadIds.Count == 0)
        {
            Console.WriteLine("No gamepads found.");
            return false;
        }
        ShowFoundGamepads(gamepadIds);
        return true;
    }

    static void ShowFoundGamepads(List<string> gamepadIds) => Console.WriteLine($"Gamepads found: {gamepadIds.Count}{Environment.NewLine}{gamepadIds.Aggregate((previous, current) => $"{previous}{Environment.NewLine}{current}")}");

}