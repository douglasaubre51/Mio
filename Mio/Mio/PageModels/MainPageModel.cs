using MvvmHelpers;

namespace Mio.PageModels;

[QueryProperty(nameof(NewProjectAdded), "NewProjectAdded")]
public partial class MainPageModel(
    ProjectStore projectStore,
    ProjectService projectServ) : BasePageModel
{
    [ObservableProperty]
    private bool newProjectAdded;

    private readonly ProjectService _projectServ = projectServ;
    private readonly ProjectStore _projectStore = projectStore;

    [ObservableProperty]
    private ProjectModel selectedProject = new();

    [ObservableProperty]
    private bool isProjectCollectionRefreshing;

    [ObservableProperty]
    private bool isPageLoading;

    [ObservableProperty]
    private ObservableRangeCollection<ProjectModel> projects = [];
    [ObservableProperty]
    private ObservableRangeCollection<ProjectModel> ongoingProjects = [];
    [ObservableProperty]
    private ObservableRangeCollection<ProjectModel> releasedProjects = [];
    [ObservableProperty]
    private ObservableRangeCollection<ProjectModel> freshProjects = [];


    [RelayCommand]
    async Task ProjectSelected()
    {
        if (SelectedProject is null || IsBusy is true) return;

        try
        {
            IsBusy = true;

            await Shell.Current.GoToAsync(
               "Details",
               true,
               new Dictionary<string, object>
               {
                {
                    "CurrentProject", SelectedProject
                }
               });
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    async Task Refreshing()
    {
        try
        {
            IsBusy = true;
            IsProjectCollectionRefreshing = true;

            await Task.Run(async () =>
            {
                _projectStore.Projects = await _projectServ.GetAll() ?? new List<ProjectModel>();

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    OngoingProjects.ReplaceRange(GetProjectByStatusUtility.GetOngoingProjects(_projectStore.Projects.ToList()).Take(8));
                    FreshProjects.ReplaceRange(GetProjectByStatusUtility.GetFreshProjects(_projectStore.Projects.ToList()).Take(8));
                    ReleasedProjects.ReplaceRange(GetProjectByStatusUtility.GetReleasedProjects(_projectStore.Projects.ToList()).Take(8));
                });
            });
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Refresh projects error: " + ex.Message);
        }
        finally
        {
            IsBusy = false;
            IsProjectCollectionRefreshing = false;
        }
    }

    async partial void OnIsPageLoadingChanged(bool oldValue, bool newValue)
    {
        if (newValue is false) return;

        try
        {
            // Bookmark changed ! Refresh state !
            if (AppStore.IsRefreshNeeded is true)
            {
                await Refreshing();
                AppStore.IsRefreshNeeded = false;

                return;
            }

            //// Load ongoing projects from cache !
            if (_projectStore.Projects.Count is not 0)
            {
                OngoingProjects.ReplaceRange(GetProjectByStatusUtility.GetOngoingProjects(_projectStore.Projects.ToList()).Take(8));
                FreshProjects.ReplaceRange(GetProjectByStatusUtility.GetFreshProjects(_projectStore.Projects.ToList()).Take(8));
                ReleasedProjects.ReplaceRange(GetProjectByStatusUtility.GetReleasedProjects(_projectStore.Projects.ToList()).Take(8));

                return;
            }

            // Block http request when device is offline!
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet) return;

            // Trigger Http GET request !
            await Refreshing();
        }
        finally
        {
            IsPageLoading = false;
        }
    }

    [RelayCommand]
    async Task GoToMoreOngoingProjects()
        => await Shell.Current.GoToAsync(
            "MoreProjects",
            true,
            new Dictionary<string, object>
            {
                {"Title", "Ongoing projects"},
                {"IsOngoing",true },
                {"Projects",GetProjectByStatusUtility.GetOngoingProjects([.. _projectStore.Projects])}
            }
            );

    [RelayCommand]
    async Task GoToFindMoreProjects()
        => await Shell.Current.GoToAsync(
            "MoreProjects",
            true,
            new Dictionary<string, object>
            {
                {"Title", "Find new project"},
                {"IsOngoing",false },
                {"Projects",GetProjectByStatusUtility.GetFreshProjects([.._projectStore.Projects])}
            }
            );

    [RelayCommand]
    async Task GoToMoreReleasedProjects()
        => await Shell.Current.GoToAsync(
            "MoreProjects",
            true,
            new Dictionary<string, object>
            {
                {"Title", "Released projects"},
                {"IsReleased",true },
                {"Projects",GetProjectByStatusUtility.GetReleasedProjects([.. _projectStore.Projects])}
            }
            );
}
