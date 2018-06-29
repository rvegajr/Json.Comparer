#tool "nuget:?package=xunit.runner.console"
//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var binDir = Directory("./bin") ;
var thisDir = System.IO.Path.GetFullPath(".") + System.IO.Path.DirectorySeparatorChar;
var slnName = "Json.Comparer.sln";
//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var buildDir = Directory("./Src/EzDbSchema.Cli/bin") + Directory(configuration);
Console.WriteLine(string.Format("target={0}", target));
Console.WriteLine(string.Format("binDir={0}", binDir));
Console.WriteLine(string.Format("thisDir={0}", thisDir));
Console.WriteLine(string.Format("buildDir={0}", buildDir));

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(buildDir);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{

	var settings = new NuGetRestoreSettings()
	{
		// VSTS has old version of Nuget.exe and Automapper restore fails because of that
		ToolPath = "./nuget/nuget.exe",
		Verbosity = NuGetVerbosity.Detailed,
	};
	NuGetRestore("./Src/" + slnName, settings);
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    if(IsRunningOnWindows())
    {
      // Use MSBuild
      MSBuild("./Src/" + slnName, settings =>
        settings.SetConfiguration(configuration));
    }
    else
    {
      // Use XBuild
      XBuild("./Src/" + slnName, settings =>
        settings.SetConfiguration(configuration));
    }
	
	if(IsRunningOnWindows())
    {
      // Use MSBuild
      MSBuild("./Src/Json.Comparer.TextResultReporter/Json.Comparer.TextResultReporter.csproj", settings =>
        settings.SetConfiguration(configuration));
    }
    else
    {
      // Use XBuild
      XBuild("./Src/Json.Comparer.TextResultReporter/Json.Comparer.TextResultReporter.csproj", settings =>
        settings.SetConfiguration(configuration));
    }

});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    Information("Start Running Tests");
    XUnit2(thisDir + @"/Src/Json.Comparer.Tests/bin/Release/" + configuration + "/*.Tests.dll", new XUnit2Settings
	{
		ReportName = "TestResults",
		Parallelism = ParallelismOption.Collections,
		HtmlReport = true,
		XmlReport = true,
		OutputDirectory = "./artifacts"
	});
});

Task("NuGet-Pack")
    .IsDependentOn("Run-Unit-Tests")
    .Does(() =>
{
   var nuGetPackSettings   = new NuGetPackSettings {
		BasePath 				= thisDir,
        Id                      = @"Json.Comparer.Core",
        Version                 = @"0.6.2",
        Title                   = @"Json.Comparer.Core",
        Authors                 = new[] {"Patrick van Lohuizen"},
        Owners                  = new[] {"Ricardo Vega Jr."},
        Description             = @"From Patrick van Lohuizen: 'Deep compares 2 objects or jtokens/jarrays/jobjects and returns the the result of the comparrison per element/property. allowing for reporting of difference between all kinds of objects'
	
	Last commit of this project was Nov 30, 2017,  so I forked it and added .net core and .net standard targets in addition to .net 4.7.2 ",
        Summary                 = @"Deep compares 2 objects or JTokens and returns the the result of the comparrison per element/property. allowing for reporting of difference between all kinds of objects.",
        ProjectUrl              = new Uri(@"https://github.com/rvegajr/Json.Comparer"),
        //IconUrl                 = new Uri(""),
        LicenseUrl              = new Uri(@"https://github.com/rvegajr/Json.Comparer/blob/master/LICENSE"),
        Copyright               = @"Patrick van Lohuizen",
        ReleaseNotes            = new [] {"Forked from Original project (much respect to Patrick van Lohuizen) and added the targets netstandard2.0;net452;net462;net472;netcoreapp1.0;netcoreapp2.0"},
        Tags                    = new [] {"json", "compare", "comparer", "newtonsoft.json", ".net", ".netcore"},
        RequireLicenseAcceptance= false,
        Symbols                 = false,
        NoPackageAnalysis       = false,
        OutputDirectory         = thisDir + "artifacts/",
		Properties = new Dictionary<string, string>
		{
			{ @"Configuration", @"Release" }
		},
		Files = new[] {
			new NuSpecContent { Source = thisDir + @"Src/Json.Comparer/bin/Release/net452/Json.Comparer.dll", Target="lib/net452"},
			new NuSpecContent { Source = thisDir + @"Src/Json.Comparer/bin/Release/net452/Json.Comparer.pdb", Target="lib/net452"},
			new NuSpecContent { Source = thisDir + @"Src/Json.Comparer/bin/Release/net462/Json.Comparer.dll", Target="lib/net462"},
			new NuSpecContent { Source = thisDir + @"Src/Json.Comparer/bin/Release/net462/Json.Comparer.pdb", Target="lib/net462"},
			new NuSpecContent { Source = thisDir + @"Src/Json.Comparer/bin/Release/net472/Json.Comparer.dll", Target="lib/net472"},
			new NuSpecContent { Source = thisDir + @"Src/Json.Comparer/bin/Release/net472/Json.Comparer.pdb", Target="lib/net472"},
			new NuSpecContent { Source = thisDir + @"Src/Json.Comparer/bin/Release/netcoreapp1.0/Json.Comparer.dll", Target="lib/netcoreapp1.0"},
			new NuSpecContent { Source = thisDir + @"Src/Json.Comparer/bin/Release/netcoreapp1.0/Json.Comparer.pdb", Target="lib/netcoreapp1.0"},
			new NuSpecContent { Source = thisDir + @"Src/Json.Comparer/bin/Release/netcoreapp2.0/Json.Comparer.dll", Target="lib/netcoreapp2.0"},
			new NuSpecContent { Source = thisDir + @"Src/Json.Comparer/bin/Release/netcoreapp2.0/Json.Comparer.pdb", Target="lib/netcoreapp2.0"},
			new NuSpecContent { Source = thisDir + @"Src/Json.Comparer/bin/Release/netstandard2.0/Json.Comparer.dll", Target="lib/netstandard2.0"},
			new NuSpecContent { Source = thisDir + @"Src/Json.Comparer/bin/Release/netstandard2.0/Json.Comparer.pdb", Target="lib/netstandard2.0"},
			new NuSpecContent { Source = thisDir + @"Src/Json.Comparer.TextResultReporter/bin/Release/Json.Comparer.TextResultReporter.dll", Target="lib/net462"},
			new NuSpecContent { Source = thisDir + @"Src/Json.Comparer.TextResultReporter/bin/Release/Json.Comparer.TextResultReporter.pdb", Target="lib/net462"},			
			
		},
		ArgumentCustomization = args => args.Append("")		
    };
            	
    NuGetPack(thisDir + "NuGet/Json.Comparer.Core.nuspec", nuGetPackSettings);
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("NuGet-Pack");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
