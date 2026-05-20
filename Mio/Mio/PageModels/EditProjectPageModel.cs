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
                await Shell.Current.DisplayAlertAsync(
                    "Validation error",
                    "Fill all the fields!",
                    "Continue");
                return;
            }

            bool result = await _projectServ.Update(new ProjectModel
            {
                Id = CurrentProject.Id,
                Title = TitleField,
                Desc = DescField,
                ShortDesc = ShortDescField,
                Dependencies = DependField,
                ProjectSpec = ProjectSpecField,
                IsFinished = IsFinished,
                IsOngoing = IsOngoing,
                IsReleased = IsReleased
            });
            if (result is false)
            {
                await Shell.Current.DisplayAlertAsync(
                    "Error",
                    "Couldnot edit project!",
                    "Continue");

                return;
            }

            TitleField = string.Empty;
            DescField = string.Empty;
            ShortDescField = string.Empty;
            DependField = string.Empty;
            ProjectSpecField = string.Empty;

            await Shell.Current.DisplayAlertAsync(
                "Success",
                "Project edited!",
                "Continue");
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
