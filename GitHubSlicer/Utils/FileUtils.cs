namespace GitHubSlicer.Utils;

public static class FileUtils
{
    public static async Task CopyStreamToFile(Stream stream,string fileAddress)
    {
        CreateFileDirectoryIfNotExists(fileAddress);
        
        await using var writer = new StreamWriter(fileAddress);
        await stream.CopyToAsync(writer.BaseStream);
    }

    public static void CreateFileDirectoryIfNotExists(string fileAddress)
    {
        var dirName = Path.GetDirectoryName(fileAddress);
        if (!Directory.Exists(dirName)) Directory.CreateDirectory(dirName);
    }
}