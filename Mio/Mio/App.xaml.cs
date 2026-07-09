[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Mio;

public partial class App : Application
{
    public App(IServiceProvider provider)
    {
        InitializeComponent();
        AppStore.Services = provider;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        Window window = new Window(new AppShell());
        window.Width = 1000;
        window.Height = 700;

        return window;
    }
}
