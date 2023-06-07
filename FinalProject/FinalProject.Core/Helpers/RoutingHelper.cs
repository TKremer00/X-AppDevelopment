namespace FinalProject.Core.Helpers
{
    public static class RoutingHelper
    {
        public static void RegisterRoute<T>() where T : Page
        {
            var type = typeof(T);
            var name = type.Name;
            Routing.RegisterRoute(name, type);
        }
    }
}
