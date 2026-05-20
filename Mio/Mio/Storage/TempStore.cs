namespace Mio.Storage;

public static class TempStore
{
    public static List<ProjectModel> Projects { get; set; } = [];
    public static List<ProjectModel> BookmarkedProjects { get; set; } = [];


    public static bool AddBookmark(ProjectModel model)
    {
        bool exists = BookmarkedProjects.Exists(project => project.Id == model.Id);
        if (exists) return false;

        BookmarkedProjects.Add(model);

        return true;
    }
}
