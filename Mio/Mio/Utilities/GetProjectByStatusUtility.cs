namespace Mio.Utilities;

public static class GetProjectByStatusUtility
{
    public static List<ProjectModel> GetFreshProjects(List<ProjectModel> Projects)
        => Projects.Where(project => project.IsFinished == false)
            .Where(project => project.IsReleased == false)
            .Where(project => project.IsOngoing == false)
            .ToList();

    public static List<ProjectModel> GetOngoingProjects(List<ProjectModel> Projects)
        => Projects.Where(project => project.IsReleased == false)
            .Where(project => project.IsOngoing == true)
            .ToList();

    public static List<ProjectModel> GetReleasedProjects(List<ProjectModel> Projects)
        => Projects.Where(project => project.IsFinished == false)
            .Where(project => project.IsReleased == true)
            .ToList();

    public static List<ProjectModel> GetFinishedProjects(List<ProjectModel> Projects)
        => Projects.Where(project => project.IsFinished == true)
            .ToList();
}
