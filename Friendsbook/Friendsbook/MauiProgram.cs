using CommunityToolkit.Maui;
using Friendsbook.Core.Controllers;
using Friendsbook.Pages;
using Friendsbook.Persistence;
using Friendsbook.Persistence.Repositories;
using Friendsbook.ViewModels;
using Friendsbook.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Friendsbook;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .RegisterDependencyIndjection()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        RegisterRoutes();

        var app = builder.Build();
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FriendsbookContext>();
        dbContext.Database.Migrate();

        return app;
    }

    public static MauiAppBuilder RegisterDependencyIndjection(this MauiAppBuilder mauiAppBuilder)
    {
        // Database
        mauiAppBuilder.Services.AddDbContext<FriendsbookContext>();
        mauiAppBuilder.Services.AddScoped<RepositoryManager>();
        mauiAppBuilder.Services.AddScoped<FriendRepository>();

        // Media
        mauiAppBuilder.Services.AddSingleton<IMediaPicker, ImagePicker>();

        // Controllers
        mauiAppBuilder.Services.AddScoped<FriendsController>();
        mauiAppBuilder.Services.AddScoped<ImageController>();

        // Pages
        mauiAppBuilder.Services.AddSingleton<MainPage>();
        mauiAppBuilder.Services.AddSingleton<FriendFormPage>();

        // View Models
        mauiAppBuilder.Services.AddTransient<MainPageViewModel>();
        mauiAppBuilder.Services.AddSingleton<FriendFormPageViewModel>();

        // Views (Only if necessary)
        mauiAppBuilder.Services.AddTransient<FriendsViewModel>();


        return mauiAppBuilder;
    }

    public static void RegisterRoutes()
    {
        RegisterRoute<MainPage>();
        RegisterRoute<FriendFormPage>();
    }

    private static void RegisterRoute<T>() where T : Page
    {
        var type = typeof(T);
        var name = type.Name;
        Routing.RegisterRoute(name, type);
    }
}
