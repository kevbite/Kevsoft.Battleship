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

Task("Default")
    .IsDependentOn("Run-Unit-Tests");

RunTarget(target);