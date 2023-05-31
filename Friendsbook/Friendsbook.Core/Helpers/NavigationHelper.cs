namespace Friendsbook.Core.Helpers
{
    public static class NavigationHelper
    {
        public static void Navigate<T>() where T : Page
        {
            var page = Application.Current.Handler.MauiContext.Services.GetService<T>();
            Application.Current.MainPage = new NavigationPage(page);
        }
    }
}
