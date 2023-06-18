using CommunityToolkit.Maui;
using FinalProject.Communication.BLEConnection;
using FinalProject.Core.Enums;
using FinalProject.Core.Helpers;
using FinalProject.Core.Services;
using FinalProject.Core.ViewModels;
using FinalProject.Pages;
using FinalProject.Persistence.Database;
using FinalProject.Persistence.Repositories;
using Material.Components.Maui.Extensions;
using Microsoft.EntityFrameworkCore;
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
            .RegisterDefaultPreferences()
            .UseMauiCommunityToolkit()
            .UseShiny()
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

        builder.Services.AddBluetoothLE();

        var app = builder.Build();

        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<PlantsContext>();
        dbContext.Database.Migrate();

        return app;
    }

    public static MauiAppBuilder RegisterDependencies(this MauiAppBuilder mauiAppBuilder)
    {
        // Database
        mauiAppBuilder.Services.AddDbContext<PlantsContext>(x => PlantsContext.GetDbContextOptions(x));
        mauiAppBuilder.Services.AddScoped<PlantRepository>();

        // Controllers
        mauiAppBuilder.Services.AddBluetoothLE<NordicThingyConnection>();

        // Services
        mauiAppBuilder.Services.AddTransient<PlantService>();

        // Pages
        mauiAppBuilder.Services.AddSingleton<MainPage>();
        mauiAppBuilder.Services.AddSingleton<SettingsPage>();
        mauiAppBuilder.Services.AddSingleton<PlantsPage>();
        mauiAppBuilder.Services.AddSingleton<AddPlantPage>();

        // View Models
        mauiAppBuilder.Services.AddTransient<MainPageViewModel>();
        mauiAppBuilder.Services.AddTransient<SettingsPageViewModel>();
        mauiAppBuilder.Services.AddTransient<PlantsPageViewModel>();
        mauiAppBuilder.Services.AddTransient<AddPlantPageViewModel>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterRoutes(this MauiAppBuilder mauiAppBuilder)
    {
        RoutingHelper.RegisterRoute<MainPage>(Routes.MainPage);
        RoutingHelper.RegisterRoute<SettingsPage>(Routes.SettingsPage);
        RoutingHelper.RegisterRoute<PlantsPage>(Routes.PlantsPage);
        RoutingHelper.RegisterRoute<AddPlantPage>(Routes.AddPlantPage);

#if DEBUG
        // NOTE: this is code to check if all routes defined in the `Routes` enum are defined.
        RoutingHelper.CheckAllRoutesRegisterd();
#endif

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterDefaultPreferences(this MauiAppBuilder mauiAppBuilder)
    {
        if (!Preferences.Default.ContainsKey(Settings.TemperatureUnit.ToString()))
        {
            Preferences.Default.Set(Settings.TemperatureUnit.ToString(), (int)TemperatureUnits.Celsius);
        }

        if (!Preferences.Default.ContainsKey(Settings.UpdateEnviromentSpeed.ToString()))
        {
            Preferences.Default.Set(Settings.UpdateEnviromentSpeed.ToString(), (int)UpdateEnviromentSpeeds.SECONDS_30);
        }

        return mauiAppBuilder;
    }
}
