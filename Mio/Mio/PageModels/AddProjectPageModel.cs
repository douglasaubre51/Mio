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
                await AlertUtility.Alert("Enter all fields !");
                return;
            }

            ProjectModel newProject = new ProjectModel
            {
                Title = TitleField,
                Desc = DescField,
                ShortDesc = ShortDescField,
                Dependencies = DependField,
                ProjectSpec = ProjectSpecField,
                IsFinished = IsFinished,
                IsReleased = IsReleased,
                IsOngoing = IsOngoing
            };
            bool result = await _projectServ.Add(newProject);
            if (result is false)
            {
                await AlertUtility.Error("Project couldnot be created !");
                return;
            }

            // Add to local state.
            AppStore.Projects.Add(newProject);
            AppStore.IsRefreshNeeded = true;

            // Clear all fields !
            TitleField = string.Empty;
            DescField = string.Empty;
            ShortDescField = string.Empty;
            DependField = string.Empty;
            ProjectSpecField = string.Empty;
            IsOngoing = false;
            IsReleased = false;
            IsFinished = false;

            await AlertUtility.Alert("New project created.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Add project error: " + ex.Message);
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
