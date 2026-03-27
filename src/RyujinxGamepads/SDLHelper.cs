using static SDL2.SDL;
static class SDLHelper
{
    internal static void InitializeSDL()
    {
        if (Initialize()<0)
        {
            throw new Exception("SDL_Init error: " + SDL_GetError());
        }
    }

    internal static void QuitSDL() => SDL_Quit();
    internal static int GetumberOfGamepads() => SDL_NumJoysticks();
    static int Initialize() => SDL_Init(SDL_INIT_GAMECONTROLLER | SDL_INIT_JOYSTICK);
    internal static Guid GetGamepadId(int gamepadIndex) => SDL_JoystickGetDeviceGUID(gamepadIndex);
}