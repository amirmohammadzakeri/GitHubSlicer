using GitHubSlicer.Model;
using GitHubSlicer.Utils;
using Serilog;

namespace GitHubSlicer;

public class GithubSlicer : IDisposable
{
    private const string GithubRawHost = "https://raw.githubusercontent.com/";
    private const string GithubHost = "https://github.com/";
    
    private readonly string _repository;
    private readonly string _branchName;
    private readonly List<Task> _filesTask = new(10000);
    
    public GithubSlicer(string repository, string branchName)
    {
        _repository = repository;
        _branchName = branchName;
    }

    public async void Clone(string directory,string destinationFolder = "")
    {
        var rootUrl = GetGithubDirectoryPath(directory);
        var metadata = GetGithubDirectoryMetadata(rootUrl);
        Log.Verbose("started fetching directory {dir}",directory);
        
        var tasks = metadata.payload.tree.items.Select(CloneItem);
        await Task.WhenAll(tasks);
        Log.Verbose("finished fetching directory {dir}",directory);

    }

    private async Task CloneFile(Item item)
    {
        FileUtils.CreateFileDirectoryIfNotExists(item.path);
        Log.Verbose("started fetching {item}",item.name);
        await DownloaderClient.DownloadToFile(GetGithubRawPath(item.path), item.path);
        Log.Logger.Verbose("finished fetching {item}",item.name);
    }
    
    private async Task CloneItem(Item item)
    {
        switch (item.contentType)
        {
            case "file":
                _filesTask.Add(CloneFile(item));
                break;
            case "directory":
                Clone(item.path);
                break;
        }
        await Task.CompletedTask;
    }
    
    private  GithubMetadata? GetGithubDirectoryMetadata(string url)
    {
        return DownloaderClient.DownloadJson<GithubMetadata>(url).Result;
    }
    
    private string GetGithubDirectoryPath(string directory)
    {
        return UrlUtils.UriCombiner(GithubHost, _repository, "tree", _branchName, directory);
    }
    
    private string GetGithubRawPath(string directory)
        {
            return UrlUtils.UriCombiner(GithubRawHost, _repository, _branchName, directory);
        }

    public void Dispose()
    {
        Task.WhenAll(_filesTask);
    }
}