namespace Mio.Storage;

public static class AppStore
{
    public static IServiceProvider? Services { get; set; }

    public static bool IsRefreshNeeded { get; set; }
}
