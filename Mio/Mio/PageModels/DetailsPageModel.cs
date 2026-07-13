namespace Mio.PageModels;

[QueryProperty(nameof(CurrentProject), "CurrentProject")]
public partial class DetailsPageModel(
    ProjectService projectServ) : BasePageModel
{
    private readonly ProjectService _projectServ = projectServ;

    [ObservableProperty]
    private ProjectModel currentProject = new();

    [ObservableProperty]
    private bool isNotBookmarked;
    [ObservableProperty]
    private bool isBookmarked;

    [ObservableProperty]
    private bool isPageLoading;


    [RelayCommand]
    async Task GoBack()
    {
        try
        {
            await Shell.Current.GoToAsync("..", true);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    [RelayCommand]
    async Task DeleteProject()
    {
        bool action = await Shell.Current.DisplayAlertAsync(
            "Delete Project",
            "Cannot undo this action!",
            "Ok",
            "Cancel");
        if (action is false)
            return;

        bool result = await _projectServ.Delete(CurrentProject.Id);
        if (result is false)
        {
            await AlertUtility.Error("Project couldnot be deleted !");
            return;
        }

        ProjectModel deletedProject = AppStore.Projects.Single(project => project.Id == CurrentProject.Id);
        AppStore.Projects.Remove(deletedProject);

        await AlertUtility.Alert("Project deleted.");

        await Shell.Current.GoToAsync(
            "///MainPage",
            true,
            new Dictionary<string, object>
            {
                    {
                        "NewProjectAdded", true
                    }
            });
    }

    [RelayCommand]
    async Task GoToEditProject()
    {
        await Shell.Current.GoToAsync(
            "EditProject",
            true,
            new Dictionary<string, object>
            {
                {
                    "CurrentProject",CurrentProject
                }
            });
    }

    [RelayCommand]
    async Task AddBookmark()
    {
        if (IsBusy is true) return;

        IsBusy = true;
        try
        {
            await _projectServ.AddBookmark(CurrentProject.Id);
            ProjectModel selectedProject = AppStore.Projects.Single(project => project.Id == CurrentProject.Id);
            selectedProject.IsBookmarked = true;

            IsNotBookmarked = false;
            IsBookmarked = true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Add bookmark error: " + ex.Message);
        }
        finally
        {
            IsBusy = false;
        }
    }
    [RelayCommand]
    async Task RemoveBookmark()
    {
        if (IsBusy is true) return;

        IsBusy = true;
        try
        {
            await _projectServ.RemoveBookmark(CurrentProject.Id);
            ProjectModel selectedProject = AppStore.Projects.Single(project => project.Id == CurrentProject.Id);
            selectedProject.IsBookmarked = false;

            IsNotBookmarked = true;
            IsBookmarked = false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Remove bookmark error: " + ex.Message);
        }
        finally
        {
            IsBusy = false;
        }
    }

    async partial void OnIsPageLoadingChanged(bool value)
    {
        if (value is false) return;

        try
        {
            IsNotBookmarked = !CurrentProject.IsBookmarked;
            IsBookmarked = CurrentProject.IsBookmarked;
        }
        finally
        {
            IsPageLoading = false;
        }
    }
}
