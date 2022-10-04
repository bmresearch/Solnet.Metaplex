#module nuget:?package=Cake.DotNetTool.Module&version=2.2.0
#addin nuget:?package=Cake.Coverlet&version=2.5.4
#tool dotnet:?package=dotnet-reportgenerator-globaltool&version=5.1.10

var testProjectsRelativePaths = new string[]
{
    "./Solnet.Metaplex.Test/Solnet.Metaplex.Test.csproj",
};

var target = Argument("target", "Pack");
var configuration = Argument("configuration", "Release");
var solutionFolder = "./";
var artifactsDir = MakeAbsolute(Directory("artifacts"));

var reportTypes = "HtmlInline";
var coverageFolder = "./code_coverage";
var coverageFileName = "results.info";

var coverageFilePath = Directory(coverageFolder) + File(coverageFileName);
var packagesDir = artifactsDir.Combine(Directory("packages"));


Task("Clean")
    .Does(() => {
        CleanDirectory(artifactsDir);
        CleanDirectory(coverageFolder);
    });

Task("Restore")
    .Does(() => {
    DotNetCoreRestore(solutionFolder);
    });

Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .Does(() => {
        DotNetCoreBuild(solutionFolder, new DotNetCoreBuildSettings
        {
            NoRestore = true,
            Configuration = configuration
        });
    });




Task("Publish")
    .IsDependentOn("Build")
    .Does(() => {
        DotNetCorePublish(solutionFolder, new DotNetCorePublishSettings
        {
            NoRestore = true,
            Configuration = configuration,
            NoBuild = true,
            OutputDirectory = artifactsDir
        });
    });

Task("Pack")
    .IsDependentOn("Publish")
    .Does(() =>
    {
        var settings = new DotNetCorePackSettings
        {
            Configuration = configuration,
            NoBuild = true,
            NoRestore = true,
            IncludeSymbols = true,
            OutputDirectory = packagesDir,
        };


        GetFiles("./src/*/*.csproj")
            .ToList()
            .ForEach(f => DotNetCorePack(f.FullPath, settings));
    });

RunTarget(target);