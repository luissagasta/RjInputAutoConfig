using System.Diagnostics;

static class ProgramRunner
{
    internal static void RunIfPathIsNotEmpty(string programFilepath, string[] args, Action doBeforeRunning = null)
    {
        if (IsEmptyPath(programFilepath))
        {
            return;
        }
        doBeforeRunning?.Invoke();
        Process.Start(programFilepath, args);
    }

    internal static bool IsEmptyPath(string programFilepath) => string.IsNullOrEmpty(programFilepath);
}