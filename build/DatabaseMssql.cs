using System;
using Nuke.Common;

partial class Build
{
    static readonly string _databaseName = "BrassTask";
    static readonly string _databaseServer = ".";

    Target DropAndRestoreMssql => _ => _
        .Before(Clean)
        .DependsOn(DropMssql, RestoreMssql)
        .Executes(() =>
        {
        });

    Target RestoreMssql => _ => _
        .Before(Clean)
        .Executes(() =>
        {
            var process = Run(Paths.RoundhouseExe,
                $"/d=\"{_databaseName}\" /f=\"{Paths.Mssql}\" /s=\"{_databaseServer}\" /cds=\"{Paths.CreateDbScriptMssql}\" /silent /transaction");
            process.WaitForExit();

            if (process.ExitCode != 0) { throw new Exception($"Problem running database migrations:\n{process.StandardOutput.ReadToEnd()}"); }
        });

    Target DropMssql => _ => _
        .Before(RestoreMssql)
        .Executes(() =>
        {
            var process = Run(Paths.RoundhouseExe, $"/d=\"{_databaseName}\" /s=\"{_databaseServer}\" /silent /drop");
            process.WaitForExit();

            if (process.ExitCode != 0) { throw new Exception($"Problem running database migrations:\n{process.StandardOutput.ReadToEnd()}"); }
        });
}
