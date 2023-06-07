using FinalProject.Core.Helpers;
using FinalProject.Pages;
using Material.Components.Maui.Extensions;
using Microsoft.Extensions.Logging;

namespace FinalProject;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .RegisterDependencies()
            .RegisterRoutes()
            .UseMaterialComponents(
                new List<string>
                {
                    "Roboto-Regular.ttf",
                    "Roboto-Italic.ttf",
                    "Roboto-Medium.ttf",
                    "Roboto-MediumItalic.ttf",
                    "Roboto-Bold.ttf",
                    "Roboto-BoldItalic.ttf",
                }
            );

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    public static MauiAppBuilder RegisterDependencies(this MauiAppBuilder mauiAppBuilder)
    {
        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterRoutes(this MauiAppBuilder mauiAppBuilder)
    {
        RoutingHelper.RegisterRoute<MainPage>();

        return mauiAppBuilder;
    }
}
