namespace GitHubSlicer.Model;

public class Payload
{
    public bool allShortcutsEnabled { get; set; }
    public string path { get; set; }
    public Repo repo { get; set; }
    public object currentUser { get; set; }
    public RefInfo refInfo { get; set; }
    public Tree tree { get; set; }
}