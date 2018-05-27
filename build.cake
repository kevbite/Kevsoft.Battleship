var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

var sln = "./Kevsoft.Battleship.sln";

Task("Clean")
    .Does(() =>
{
    DotNetCoreClean(sln);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    DotNetCoreRestore(sln);
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    DotNetCoreBuild(sln, new DotNetCoreBuildSettings
    {
        Configuration = configuration
    });
});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    var settings = new DotNetCoreTestSettings
    {
        Configuration = configuration
    };
    var projects = GetFiles("./test/**/*.csproj");
    foreach(var project in projects)
    {
        DotNetCoreTest(project.FullPath, settings);
    }
});

Task("Create-Artifacts")
    .IsDependentOn("Run-Unit-Tests")
    .Does(() =>
{
    void CreateArtifacts(string runtime){
        var settings = new DotNetCorePublishSettings
        {
            Framework = "netcoreapp2.0",
            Configuration = configuration,
            OutputDirectory = $"./artifacts/Kevsoft.Battleship.ConsoleApp-{runtime}",
            SelfContained = true,
            Runtime = runtime
        };

        DotNetCorePublish("./src/Kevsoft.Battleship.ConsoleApp/Kevsoft.Battleship.ConsoleApp.csproj", settings);
    }
    CreateArtifacts("win-x64");
    CreateArtifacts("osx-x64");
    CreateArtifacts("linux-x64");
});

Task("Default")
    .IsDependentOn("Create-Artifacts");

RunTarget(target);