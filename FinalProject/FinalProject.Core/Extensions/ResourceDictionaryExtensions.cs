namespace FinalProject.Core.Extensions
{
    public static class ResourceDictionaryExtensions
    {
        public static T FindValueRecursive<T>(this ResourceDictionary resourceDict, string key)
        {
            foreach (var resourceDictionary in resourceDict.MergedDictionaries.Prepend(resourceDict))
            {
                if (resourceDictionary.TryGetValue(key, out var value))
                {
                    return (T)value;
                }
            }

            throw new Exception("could not find resource");
        }
    }
}
