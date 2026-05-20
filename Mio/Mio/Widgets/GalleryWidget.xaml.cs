namespace Mio.Widgets;

public partial class GalleryWidget : CollectionView
{

    public static BindableProperty ItemsProperty = BindableProperty.Create(
        nameof(Items),
        typeof(List<ProjectModel>),
        typeof(GalleryWidget),
        new List<ProjectModel>(),
        propertyChanged: (context, oldValue, newValue) =>
        {
        }
        );

    public List<ProjectModel> Items
    {
        get => (List<ProjectModel>)GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }

    public GalleryWidget()
    {
        InitializeComponent();
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
    }
}