using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Nuke.Common;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;

partial class Build : NukeBuild
{
    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
        });

    Target Restore => _ => _
        .Executes(() =>
        {
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
        });

    Target InstallTools => _ => _
        .Executes(() =>
        {
            if (!Directory.Exists(Paths.Tools)) { Directory.CreateDirectory(Paths.Tools); }

            var tools = DotNetTasks.DotNet($"tool list --tool-path {Paths.Tools}");

            InstallOrUpdateTool(tools, "dotnet-roundhouse");
        });

    public static int Main() => Execute<Build>(x => x.Compile);

    void InstallOrUpdateTool(IReadOnlyCollection<Output> tools, string toolName)
    {
        if (tools.Any(x => x.Text.StartsWith(toolName))) { DotNetTasks.DotNetToolUpdate(_ => _.SetPackageName(toolName).SetToolInstallationPath(Paths.Tools)); }
        else { DotNetTasks.DotNetToolInstall(_ => _.SetPackageName(toolName).SetToolInstallationPath(Paths.Tools)); }
    }

    Process Run(string exePath, string args = null, bool fromOwnDirectory = false)
    {
        string directory = null;

        if (fromOwnDirectory) { directory = Directory.GetParent(exePath).FullName; }

        return Run(exePath, args, directory);
    }

    Process Run(string exePath, string args, string workingDirectory)
    {
        var startInfo = new ProcessStartInfo(exePath);

        if (workingDirectory != null) { startInfo.WorkingDirectory = workingDirectory; }

        startInfo.Arguments = args ?? string.Empty;
        startInfo.UseShellExecute = false;

        var process = Process.Start(startInfo);

        return process;
    }
}
