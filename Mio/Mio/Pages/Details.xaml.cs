namespace Mio.Pages;

public partial class Details : ContentPage
{
    public Details(DetailsPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var context = BindingContext as DetailsPageModel;
        context!.IsPageLoading = true;
    }
}