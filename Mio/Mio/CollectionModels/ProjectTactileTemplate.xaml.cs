namespace Mio.CollectionModels;

public partial class ProjectTactileTemplate : DataTemplate
{
    public ProjectTactileTemplate()
    {
        InitializeComponent();
    }

    private async void ProjectSelectionChanged(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var project = button.BindingContext as ProjectModel;

        if (project is null) return;

        await Shell.Current.GoToAsync(
            "Details",
            true,
            new Dictionary<string, object>
            {
                {"CurrentProject",project }
            });
    }
}