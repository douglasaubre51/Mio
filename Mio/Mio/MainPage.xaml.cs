namespace Mio;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var context = BindingContext as MainPageModel;

        Dispatcher.Dispatch(() =>
            context!.IsPageLoading = true
        );
    }

}
