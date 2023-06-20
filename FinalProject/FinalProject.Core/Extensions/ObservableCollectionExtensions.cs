using System.Collections.ObjectModel;

namespace FinalProject.Core.Extensions
{
    public static class ObservableCollectionExtensions
    {
        public static void AddAndRemoveFirst<T>(this ObservableCollection<T> observableList, int maxCapacity, T item)
        {
            if (observableList.Count > maxCapacity)
            {
                observableList.RemoveAt(0);
            }

            observableList.Add(item);
        }
    }
}
