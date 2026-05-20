namespace Mio.Pages;

public partial class Saved : ContentPage
{
    public Saved(SavedPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var context = BindingContext as SavedPageModel;
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