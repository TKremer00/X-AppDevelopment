using CommunityToolkit.Maui;
using Friendsbook.Core.Controllers;
using Friendsbook.Persistence;
using Friendsbook.Persistence.Repositories;
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

        var app = builder.Build();
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FriendsbookContext>();
        dbContext.Database.Migrate();

        return app;
    }

    public static MauiAppBuilder RegisterDependencyIndjection(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddDbContext<FriendsbookContext>();
        mauiAppBuilder.Services.AddScoped<RepositoryManager>();
        mauiAppBuilder.Services.AddScoped<FriendRepository>();

        mauiAppBuilder.Services.AddScoped<FriendsController>();

        return mauiAppBuilder;
    }
}
