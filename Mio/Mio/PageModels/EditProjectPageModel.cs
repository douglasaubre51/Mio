namespace Mio.PageModels;

[QueryProperty(nameof(CurrentProject), "CurrentProject")]
public partial class EditProjectPageModel(
    ProjectService projectServ
    ) : BasePageModel
{
    [ObservableProperty]
    private ProjectModel currentProject = new();

    [ObservableProperty]
    private bool isPageLoading;

    private readonly ProjectService _projectServ = projectServ;

    [ObservableProperty]
    private string titleField = string.Empty;

    [ObservableProperty]
    private string descField = string.Empty;

    [ObservableProperty]
    private string shortDescField = string.Empty;

    [ObservableProperty]
    private string dependField = string.Empty;

    [ObservableProperty]
    private string projectSpecField = string.Empty;

    [ObservableProperty]
    private bool isOngoing;
    [ObservableProperty]
    private bool isReleased;
    [ObservableProperty]
    private bool isFinished;

    partial void OnIsPageLoadingChanged(bool oldValue, bool newValue)
    {
        if (newValue is false) return;

        try
        {
            TitleField = CurrentProject.Title;
            DescField = CurrentProject.Desc;
            ShortDescField = CurrentProject.ShortDesc;
            DependField = CurrentProject.Dependencies;
            ProjectSpecField = CurrentProject.ProjectSpec;
            IsOngoing = CurrentProject.IsOngoing;
            IsFinished = CurrentProject.IsFinished;
            IsReleased = CurrentProject.IsReleased;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            IsPageLoading = false;
        }
    }

    [RelayCommand]
    async Task EditProject()
    {
        IsBusy = true;

        try
        {
            if (
                string.IsNullOrWhiteSpace(TitleField)
                || string.IsNullOrWhiteSpace(DescField)
                || string.IsNullOrWhiteSpace(ShortDescField)
                || string.IsNullOrWhiteSpace(DependField)
                || string.IsNullOrWhiteSpace(ProjectSpecField))
            {
                await AlertUtility.Alert("Enter all fields !");
                return;
            }

            ProjectModel editedProject = new ProjectModel
            {
                Id = CurrentProject.Id,
                Title = TitleField,
                Desc = DescField,
                ShortDesc = ShortDescField,
                Dependencies = DependField,
                ProjectSpec = ProjectSpecField,
                IsFinished = IsFinished,
                IsOngoing = IsOngoing,
                IsReleased = IsReleased,
                IsBookmarked = CurrentProject.IsBookmarked
            };
            bool result = await _projectServ.Update(editedProject);
            if (result is false)
            {
                await AlertUtility.Error("Project couldnot be edited !");
                return;
            }

            ProjectModel toBeUpdatedProject = AppStore.Projects.Single(project => project.Id == CurrentProject.Id);
            AppStore.Projects.Remove(toBeUpdatedProject);
            AppStore.Projects.Add(editedProject);

            await AlertUtility.Alert("Project has been edited.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlertAsync(
                "Error",
                "Error occured during project edit!",
                "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }

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
}
