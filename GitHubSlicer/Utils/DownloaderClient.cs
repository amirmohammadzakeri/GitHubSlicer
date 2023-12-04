using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace GitHubSlicer.Utils;

using static FileUtils;

public static class DownloaderClient
{
    private static readonly HttpClient DownloaderHttpClient = new()
    {
        DefaultRequestHeaders = { Accept = { MediaTypeWithQualityHeaderValue.Parse("application/json") } }
    };

    public static async Task DownloadToFile(string url, string destination)
    {
        await Download(url)
            .ContinueWith(task => CopyStreamToFile(task.Result, destination))
            .Result;
    }

    private static async Task<Stream> Download(string url)
    {
        return await DownloaderHttpClient.GetStreamAsync(url);
    }

    public static async Task<T?> DownloadJson<T>(string url)
    {
        return await DownloaderHttpClient.GetFromJsonAsync<T>(url);
    }
}