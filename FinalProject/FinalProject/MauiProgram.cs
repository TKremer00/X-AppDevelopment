using CommunityToolkit.Maui;
using FinalProject.Core.Helpers;
using FinalProject.Core.ViewModels;
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
            .UseMauiCommunityToolkit()
            .UseMaterialComponents(
                new List<string>
                {
                    //"Aloha.ttf",
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
        // Database

        // Controllers

        // Pages
        mauiAppBuilder.Services.AddSingleton<MainPage>();

        // View Models
        mauiAppBuilder.Services.AddTransient<MainPageViewModel>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterRoutes(this MauiAppBuilder mauiAppBuilder)
    {
        RoutingHelper.RegisterRoute<MainPage>();

        return mauiAppBuilder;
    }
}
