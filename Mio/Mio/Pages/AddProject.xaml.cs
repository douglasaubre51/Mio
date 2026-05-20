namespace Mio.Pages;

public partial class AddProject : ContentPage
{
    public AddProject(AddProjectPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }
}