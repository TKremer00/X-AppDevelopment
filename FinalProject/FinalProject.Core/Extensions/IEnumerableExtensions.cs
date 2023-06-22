using FinalProject.Core.ObservableModels;

namespace FinalProject.Core.Extensions
{
    public static class IEnumerableExtensions<T>
    {

    }

    public static class PlantsIEnumerableExtensions
    {
        public static void UpdateAmbient(this IEnumerable<ObservablePlant> plants, Action<ObservablePlant, int> updateAction, int value)
        {
            foreach (var plant in plants)
            {
                updateAction(plant, value);
            }
        }
    }
}
