using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace GitHubSlicer;

public static class Program
{
    public static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddCommandLine(args).Build();
        
        var verboseLogging = configuration.AsEnumerable().Any(val => val.Key.ToLower() == "verbose");
        
        if (verboseLogging)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console() 
                .CreateLogger();
        }
        else
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console() 
                .CreateLogger();
        }
        
        var repo = configuration["repo"] ?? "apache/nifi";
        var branch = configuration["branch"] ?? "main";
        var directory = configuration["dir"] ?? "nifi-nar-bundles/nifi-framework-bundle/nifi-framework/nifi-web/nifi-web-frontend/src/main/nifi";
        
        using var gitSlicer = new GithubSlicer(repo, branch);
        gitSlicer.Clone(directory);
    }
}