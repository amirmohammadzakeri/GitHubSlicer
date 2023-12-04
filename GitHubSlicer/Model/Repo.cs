namespace GitHubSlicer.Model;

public class Repo
{
    public int id { get; set; }
    public string defaultBranch { get; set; }
    public string name { get; set; }
    public string ownerLogin { get; set; }
    public bool currentUserCanPush { get; set; }
    public bool isFork { get; set; }
    public bool isEmpty { get; set; }
    public string createdAt { get; set; }
    public string ownerAvatar { get; set; }
    public bool isOrgOwned { get; set; }
}