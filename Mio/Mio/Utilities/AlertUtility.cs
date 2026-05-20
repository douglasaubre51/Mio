namespace Mio.Utilities;

public static class AlertUtility
{
    public static async Task Alert(string message)
        => await Shell.Current.DisplayAlertAsync(
            "Alert",
            message,
            "Ok");

    public static async Task<bool> Warning(string message)
        => await Shell.Current.DisplayAlertAsync(
            "Warning",
            message,
            "Continue",
            "Cancel");

    public static async Task Error(string message)
        => await Shell.Current.DisplayAlertAsync(
            "Error",
            message,
            "Ok");
}
