using System;
using System.Collections.Generic;
using System.Linq;
using Nuke.Common;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;

partial class Build
{
    Target SetupUserSecrets =>
        _ => _
            .Executes(() =>
            {
                var secrets = DotNetTasks.DotNet($"user-secrets list", Paths.ApiCsProject);

                SetUserSecretIfMissing(secrets, "Serilog:WriteTo:2:Args:apiKey", "<replace with Serilog API key>");
                SetUserSecretIfMissing(secrets, "Jwt:Key", Guid.NewGuid().ToString("N"));
            });

    void SetUserSecretIfMissing(IReadOnlyCollection<Output> secrets, string secretName, string secretValue)
    {
        if (!secrets.Any(x => x.Text.StartsWith(secretName)))
        {
            DotNetTasks.DotNet($"user-secrets set \"{secretName}\" \"{secretValue}\"", Paths.ApiCsProject);
        }
    }
}
