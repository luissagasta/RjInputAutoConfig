using static SDLHelper;

class GamepadsHelper
{
    internal static List<string> GetGamepadRiujynxIds()
    {
        InitializeSDL();
        int numJoysticks = GetumberOfGamepads();
        var gamepadsIds = new List<string>();
        for (int i = 0; i < numJoysticks; i++)
        {
            if (!TryGenerateGamepadId(i, gamepadsIds, out var gamepadId))
            {
                continue;
            }
            gamepadsIds.Add(gamepadId);
        }
        QuitSDL();
        return gamepadsIds;
    }

    static bool TryGenerateGamepadId(int gamepadIndex, List<string> existingGamepadIds, out string id)
    {
        var gamepadId = GetGamepadId(gamepadIndex);
        if (gamepadId == Guid.Empty)
        {
            id = null;
            return false;
        }
        id = GetRyujinxId(gamepadId, existingGamepadIds);
        return true;
    }

    static string GetRyujinxId(Guid gamepadId, List<string> existingGamepadIds)
    {
        int guidIndex = 0;
        var id = $"{guidIndex}-{gamepadId}";
        while (existingGamepadIds.Contains(id))
        {
            id = $"{++guidIndex}-{gamepadId}";
        }
        return id;
    }
}