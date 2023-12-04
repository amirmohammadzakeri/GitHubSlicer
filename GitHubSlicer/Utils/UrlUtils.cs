namespace GitHubSlicer;

public static class UrlUtils
{
    private const char Separator = '/';
    
    public static Uri UriCombiner(params Uri[] uris)
    {
        var result = uris.Aggregate(
            (current, next) =>
            {
                Console.WriteLine($"{current} , {next}");
                return new Uri(current, next);
            }
            );
        return result;
    }
    
    public static string UriCombiner(params string[] uris)
    {
        var result = uris.Aggregate(
            (current, next) =>
            {
                var first = current.TrimEnd(Separator);
                var last = next.TrimStart(Separator);
                return string.Join(Separator, first, last);
            }
        );
        return result;
    }
}