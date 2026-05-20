using MvvmHelpers;

namespace Mio.PageModels;

public partial class SearchPageModel(
    ProjectService projectServ) : BasePageModel
{
    private readonly ProjectService _projectServ = projectServ;

    [ObservableProperty]
    private bool isPageLoading;

    [ObservableProperty]
    private ObservableRangeCollection<ProjectModel> projects = [];

    [ObservableProperty]
    private string searchText = string.Empty;

    partial void OnIsPageLoadingChanged(bool value)
    {
        if (value is false) return;

        isPageLoading = false;
    }

    [RelayCommand]
    async Task SearchProject()
    {
        if (string.IsNullOrWhiteSpace(SearchText)) return;

        if (IsBusy is true) return;

        IsBusy = true;
        try
        {
            Projects.Clear();
            var projects = await _projectServ.Search(SearchText);
            if (projects.Count is 0) return;

            Projects.ReplaceRange(projects);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Search failed: " + ex.Message);
            await AlertUtility.Error("Error searching for project!");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
