namespace Mio;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Register routes

        Routing.RegisterRoute("Details", typeof(Details));
        Routing.RegisterRoute("EditProject", typeof(EditProject));
        Routing.RegisterRoute("MoreProjects", typeof(MoreProjects));
    }
}
