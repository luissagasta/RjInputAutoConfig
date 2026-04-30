using System.Diagnostics;

internal static class ProgramRunner
{
    internal static void RunIfPathIsNotEmpty(string programFilepath, string[] args)
    {
        if(string.IsNullOrEmpty(programFilepath))
        {
            return;
        }
        Process.Start(programFilepath, args);
    }

    static void Run(string programFilepath, string[] args) => Process.Start(programFilepath, args);
}