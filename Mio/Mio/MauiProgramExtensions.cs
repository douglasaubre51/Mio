
using Mio.PageModels.ProjectsPageModels;

namespace Mio;

public static class MauiProgramExtensions
{
    public static MauiAppBuilder UseSharedMauiApp(this MauiAppBuilder builder)
    {
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Poppins-Bold.ttf", "PoppinsBold");
                fonts.AddFont("Poppins-BoldItalic.ttf", "PoppinsBoldItalic");
                fonts.AddFont("Poppins-ExtraBold.ttf", "PoppinsExtraBold");
                fonts.AddFont("Poppins-ExtraBoldItalic.ttf", "PoppinsExtraBoldItalic");
                fonts.AddFont("Poppins-Italic.ttf", "PoppinsItalic");
                fonts.AddFont("Poppins-Medium.ttf", "PoppinsMedium");
                fonts.AddFont("Poppins-MediumItalic.ttf", "PoppinsMediumItalic");
                fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
                fonts.AddFont("Poppins-SemiBold.ttf", "PoppinsSemiBold");
                fonts.AddFont("Poppins-SemiBoldItalic.ttf", "PoppinsSemiBoldItalic");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // Add Services

        builder.Services.AddSingleton<ProjectStore>();
        builder.Services.AddSingleton<ProjectService>();


        // Add PageModels

        builder.Services.AddTransient<EditProjectPageModel>();
        builder.Services.AddTransient<DetailsPageModel>();
        builder.Services.AddTransient<AddProjectPageModel>();
        builder.Services.AddTransient<MainPageModel>();
        builder.Services.AddTransient<MoreProjectsPageModel>();
        builder.Services.AddTransient<SavedPageModel>();
        builder.Services.AddTransient<FinishedPageModel>();
        builder.Services.AddTransient<SearchPageModel>();

        return builder;
    }
}
