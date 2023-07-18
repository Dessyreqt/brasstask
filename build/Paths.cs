using Nuke.Common;
using Nuke.Common.IO;

public class Paths
{
    // Tools
    public static readonly AbsolutePath Tools = NukeBuild.RootDirectory / "bin";
    public static readonly AbsolutePath RoundhouseExe = Tools / "rh.exe";

    // C# API
    public static readonly AbsolutePath ApiCsProject = NukeBuild.RootDirectory / "api" / "cs" / "BrassTask.Api";

    // T-SQL database files
    public static readonly AbsolutePath Mssql = NukeBuild.RootDirectory / "db" / "mssql";
    public static readonly AbsolutePath CreateDbScriptMssql = Mssql / "create.sql";
}
