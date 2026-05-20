using MvvmHelpers;

namespace Mio.PageModels;

public partial class FinishedPageModel(
    ProjectService projectServ) : BasePageModel
{
    private readonly ProjectService _projectServ = projectServ;

    [ObservableProperty]
    private ObservableRangeCollection<ProjectModel> projects = [];
    [ObservableProperty]
    private bool isPageLoading;


    async partial void OnIsPageLoadingChanged(bool value)
    {
        if (value is false) return;

        IsBusy = true;
        try
        {
            Projects.Clear();
            List<ProjectModel> allProjects = await _projectServ.GetAll() ?? [];
            if (allProjects.Count is 0) return;

            Projects.ReplaceRange(GetProjectByStatusUtility.GetFinishedProjects(allProjects));
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Saved page load error: " + ex.Message);
        }
        finally
        {
            IsBusy = false;
            IsPageLoading = false;
        }
    }
}
