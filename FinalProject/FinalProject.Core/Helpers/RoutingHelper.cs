using FinalProject.Core.Enums;

namespace FinalProject.Core.Helpers
{
    public static class RoutingHelper
    {
        private static readonly IDictionary<Routes, string> _routes;
        public static event EventHandler<RoutEventArgs> RoutingHelperNavigationChanged;
        private static Routes _previouseRoute = Routes.MainPage;


        static RoutingHelper()
        {
            _routes = new Dictionary<Routes, string>();
        }

        public static void RegisterRoute<T>(Routes page) where T : Page
        {
            var type = typeof(T);
            var name = type.Name;
            _routes.Add(page, name);

            Routing.RegisterRoute(name, type);
        }

#if DEBUG
        public static void CheckAllRoutesRegisterd()
        {
            var values = Enum.GetValues<Routes>();
            foreach (var value in values)
            {
                if (!_routes.ContainsKey(value))
                {
                    throw new InvalidNavigationException($"Route {value} is not defined");
                }
            }
        }
#endif

        internal static async Task NavigateToAsync(Routes page)
        {
            RoutingHelperNavigationChanged?.Invoke(null, new RoutEventArgs { From = _previouseRoute, To = page });
            _previouseRoute = page;

            await Shell.Current.GoToAsync(_routes[page], true);
        }

        internal static async Task NavigateBackAsync()
        {
            RoutingHelperNavigationChanged.Invoke(null, new RoutEventArgs { From = _previouseRoute });

            await Shell.Current.GoToAsync("..");
        }
    }

    public class RoutEventArgs : EventArgs
    {
        public Routes From { get; set; }

        public Routes? To { get; set; }
    }
}
