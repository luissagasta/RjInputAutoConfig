static class MessagesHelper
{
    internal static void ShowError(Exception exception) => Show($"An error occurred: {exception.Message}", ConsoleColor.Red);
    internal static void ShowSuccess(string message) => Show(message, ConsoleColor.Green);
    internal static void ShowWarning(string message) => Show(message, ConsoleColor.Yellow);
    internal static void Show(string message) => Console.WriteLine(message);
    
    static void Show(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Show(message);
        Console.ResetColor();
    }
}