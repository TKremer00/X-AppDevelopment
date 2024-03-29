﻿using CommunityToolkit.Maui;
using FinalProject.Communication.Communication;
using FinalProject.Core.Converters;
using FinalProject.Core.Helpers;
using FinalProject.Core.Services;
using FinalProject.Core.ViewModels;
using FinalProject.Data.Enums;
using FinalProject.Data.Interfaces;
using FinalProject.Pages;
using FinalProject.Persistence.Database;
using FinalProject.Persistence.Extension;
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
            .RegisterDefaultPreferences()
            .RegisterDependencies()
            .RegisterRoutes()
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
        _ = dbContext.Database.SeedIfEmpty(scope.ServiceProvider);

        return app;
    }

    public static MauiAppBuilder RegisterDependencies(this MauiAppBuilder mauiAppBuilder)
    {
        // Database
        mauiAppBuilder.Services.AddDbContext<PlantsContext>(x => PlantsContext.GetDbContextOptions(x));
        mauiAppBuilder.Services.AddScoped<PlantRepository>();
        mauiAppBuilder.Services.AddScoped<TemperatureRepository>();

        // Connections
        mauiAppBuilder.Services.AddSingleton<IBluetoothNotifier, BluetoothNotifier>();

        // Services
        mauiAppBuilder.Services.AddTransient<PlantService>();
        mauiAppBuilder.Services.AddTransient<TemperatureService>();

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

        // Converters
        mauiAppBuilder.Services.AddSingleton<AmbiantEnviromentStatusToColorConverter>();
        mauiAppBuilder.Services.AddSingleton<BooleanInverterConverter>();
        mauiAppBuilder.Services.AddSingleton<TemperatureConverter>();

        // Preferences
        mauiAppBuilder.Services.AddSingleton<IPreferencesWrapper>(new PreferenceWrapper(Preferences.Default));

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
