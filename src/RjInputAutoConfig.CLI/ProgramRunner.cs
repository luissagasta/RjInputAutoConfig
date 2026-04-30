using System.Diagnostics;

internal class ProgramRunner
{
    internal static void RunIfPathIsNotEmpty(string programFilepath, string[] args, Action doBeforeRunning = null)
    {
        if(IsEmptyPath(programFilepath))
        {
            return;
        }
        doBeforeRunning?.Invoke();
        Process.Start(programFilepath, args);
    }

    internal static bool IsEmptyPath(string programFilepath) => string.IsNullOrEmpty(programFilepath);

    static void Run(string programFilepath, string[] args) => Process.Start(programFilepath, args);
}