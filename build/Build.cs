using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nuke.Common;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;

class Build : NukeBuild
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

    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode
    public static int Main() => Execute<Build>(x => x.Compile);

    void InstallOrUpdateTool(IReadOnlyCollection<Output> tools, string toolName)
    {
        if (tools.Any(x => x.Text.StartsWith(toolName))) { DotNetTasks.DotNet($"tool update {toolName} --tool-path {Paths.Tools}"); }
        else { DotNetTasks.DotNet($"tool install {toolName} --tool-path {Paths.Tools}"); }
    }
}
