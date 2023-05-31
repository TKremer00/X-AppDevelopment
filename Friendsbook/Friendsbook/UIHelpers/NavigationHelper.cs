namespace Friendsbook.UIHelpers
{
    public static class NavigationHelper
    {
        public static async Task NavigateTo<T>() where T : Page
        {
            var name = typeof(T).Name;
            await Shell.Current.GoToAsync(name);
        }
    }
}
