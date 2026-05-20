using Mio.PageModels.ProjectsPageModels;

namespace Mio.Pages.Projects;

public partial class MoreProjects : ContentPage
{
    public MoreProjects(MoreProjectsPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var context = BindingContext as MoreProjectsPageModel;
        context!.IsPageLoading = true;
    }

    private async void ProjectSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ProjectModel project = (ProjectModel)e.CurrentSelection.SingleOrDefault()!;
        if (project is null) return;

        await Shell.Current.GoToAsync(
            "Details",
            false,
            new Dictionary<string, object>
            {
                {"CurrentProject",project }
            });
    }
}