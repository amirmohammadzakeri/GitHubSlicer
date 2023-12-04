namespace GitHubSlicer.Model;

public class RefInfo
{
    public string name { get; set; }
    public string listCacheKey { get; set; }
    public bool canEdit { get; set; }
    public string refType { get; set; }
    public string currentOid { get; set; }
}