namespace Mio.CollectionModels.SavedTemplates;

/*
    NOTE:
    Sometimes my genius is almost frightening!
 * 
 */

public partial class ProjectListTemplate : DataTemplate
{
    private readonly ProjectService _projectServ;

    public ProjectListTemplate()
    {
        InitializeComponent();
        _projectServ = AppStore.Services!.GetService<ProjectService>()!;
    }

    private async void RemoveBookmarkButtonClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var project = (ProjectModel)button.BindingContext;

        // Climb the ladder till u reach its parent page binding context !
        var context = button.Parent.Parent.Parent.Parent.BindingContext as SavedPageModel;

        bool status = await _projectServ.RemoveBookmark(project.Id);
        if (status is false)
        {
            Debug.WriteLine("Bookmark couldnot be removed!");
            await AlertUtility.Alert("Couldnot remove bookmark!");
            return;
        }

        Debug.WriteLine("remove bookmark btn clicked !");
        Debug.WriteLine("Bookmark Title: " + project.Title);

        // To refresh home page !
        AppStore.IsRefreshNeeded = true;

        context!.Projects.Remove(project);
    }
}