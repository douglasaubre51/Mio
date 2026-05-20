
namespace Mio.PageModels;

public partial class BasePageModel : ObservableObject
{
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    [ObservableProperty]
    private bool isBusy;

    public bool IsNotBusy => !IsBusy;
}
