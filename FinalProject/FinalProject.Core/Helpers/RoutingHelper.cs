using FinalProject.Data.Enums;

namespace FinalProject.Core.Helpers
{
    public static class RoutingHelper
    {
        private static readonly IDictionary<Routes, string> _routes;
        public static event EventHandler<RoutEventArgs> RoutingHelperNavigationChanged;
        private static readonly Stack<Routes> _previouseRoutes;

        static RoutingHelper()
        {
            _routes = new Dictionary<Routes, string>();
            _previouseRoutes = new Stack<Routes>();
            _previouseRoutes.Push(Routes.MainPage);
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
            var eventsArgs = new RoutEventArgs { From = _previouseRoutes.Peek(), To = page };
            RoutingHelperNavigationChanged?.Invoke(null, eventsArgs);
            _previouseRoutes.Push(page);

            await Shell.Current.GoToAsync(_routes[page], true);
        }

        internal static async Task NavigateBackAsync()
        {
            NavigateBackUpdate();
            await Shell.Current.GoToAsync("..");
        }

        public static void NavigateBackUpdate()
        {
            var eventsArgs = new RoutEventArgs { From = _previouseRoutes.Pop(), To = _previouseRoutes.Peek() };
            RoutingHelperNavigationChanged.Invoke(null, eventsArgs);

            if (!_previouseRoutes.Any())
            {
                _previouseRoutes.Push(Routes.MainPage);
            }
        }
    }

    public class RoutEventArgs : EventArgs
    {
        public Routes From { get; set; }

        public Routes? To { get; set; }
    }
}
