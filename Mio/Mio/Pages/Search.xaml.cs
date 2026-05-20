namespace Mio.Pages;

public partial class Search : ContentPage
{
    public Search(SearchPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var context = BindingContext as SearchPageModel;
        context!.IsPageLoading = true;
    }

    private async void ProjectSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ProjectModel project = (ProjectModel)e.CurrentSelection.SingleOrDefault()!;
        if (project is null) return;

        await Shell.Current.GoToAsync(
            "Details",
            true,
            new Dictionary<string, object>
            {
                {"CurrentProject",project }
            });

        var collectionView = (CollectionView) sender;
        collectionView.SelectedItem = null;
    }
}