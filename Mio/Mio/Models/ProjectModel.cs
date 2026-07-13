namespace Mio.Models;

public class ProjectModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ProjectSpec { get; set; } = string.Empty;
    public string Dependencies { get; set; } = string.Empty;
    public string ShortDesc { get; set; } = string.Empty;
    public string Desc { get; set; } = string.Empty;

    public bool IsOngoing { get; set; }
    public bool IsReleased { get; set; }
    public bool IsFinished { get; set; }

    public bool IsBookmarked { get; set; }
}
