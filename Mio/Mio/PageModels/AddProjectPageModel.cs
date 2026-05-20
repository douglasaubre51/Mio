namespace Mio.PageModels;

public partial class AddProjectPageModel(
    ProjectService projectServ) : BasePageModel
{
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

    [RelayCommand]
    async Task NewProject()
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
                await AlertUtility.Alert("Enter all fields!");
                return;
            }

            bool result = await _projectServ.Add(new ProjectModel
            {
                Title = TitleField,
                Desc = DescField,
                ShortDesc = ShortDescField,
                Dependencies = DependField,
                ProjectSpec = ProjectSpecField,
                IsFinished = IsFinished,
                IsReleased = IsReleased,
                IsOngoing = IsOngoing
            });
            if (result is false)
            {
                await Shell.Current.DisplayAlertAsync(
                    "Success",
                    "Added new project to database!",
                    "Continue");

                return;
            }

            TitleField = string.Empty;
            DescField = string.Empty;
            ShortDescField = string.Empty;
            DependField = string.Empty;
            ProjectSpecField = string.Empty;
            IsOngoing = false;
            IsReleased = false;
            IsFinished = false;

            await Shell.Current.DisplayAlertAsync(
                "Success",
                "Added new project to database!",
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
                "Error occured during project creation!",
                "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
