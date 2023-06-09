using FinalProject.Core.Enums;

namespace FinalProject.Core.Helpers
{
    public static class RoutingHelper
    {
        private static readonly IDictionary<Routes, string> _routes;


        static RoutingHelper()
        {
            _routes = new Dictionary<Routes, string>();
        }

        public static void RegisterRoute<T>(Routes page)
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
            await Shell.Current.GoToAsync(_routes[page], true);
        }

        internal static async Task NavigateBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
