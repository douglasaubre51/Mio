using MvvmHelpers;

namespace Mio.PageModels.ProjectsPageModels;

[QueryProperty(nameof(PageTitle), "Title")]
[QueryProperty(nameof(IsOngoing), "IsOngoing")]
[QueryProperty(nameof(IsReleased), "IsReleased")]
[QueryProperty(nameof(HoldProjects), "Projects")]
public partial class MoreProjectsPageModel : BasePageModel
{
    [ObservableProperty]
    private string pageTitle = string.Empty;
    [ObservableProperty]
    private bool isOngoing;
    [ObservableProperty]
    private bool isReleased;
    [ObservableProperty]
    private ObservableRangeCollection<ProjectModel> projects = [];

    [ObservableProperty]
    private List<ProjectModel> holdProjects = [];

    [ObservableProperty]
    private bool isPageLoading;


    async partial void OnIsPageLoadingChanged(bool value)
    {
        if (value is false) return;

        try
        {
            Projects.ReplaceRange(HoldProjects);
        }
        finally
        {
            IsPageLoading = false;
        }
    }

    [RelayCommand]
    async Task GoBackHome()
        => await Shell.Current.GoToAsync("..", false);
}
