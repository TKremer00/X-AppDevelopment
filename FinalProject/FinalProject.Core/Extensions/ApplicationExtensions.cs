namespace FinalProject.Core.Extensions
{
    public static class ApplicationExtensions
    {
        public static T GetRequiredService<T>(this Application app)
        {
            return app.Handler.MauiContext.Services.GetRequiredService<T>();
        }
    }
}
