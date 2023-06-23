using FinalProject.Data.Interfaces;

namespace FinalProject.Core.Helpers
{
    public class PreferenceWrapper : IPreferencesWrapper
    {
        private readonly IPreferences _preferences;

        public PreferenceWrapper(IPreferences preferences)
        {
            _preferences = preferences;
        }

        public bool ContainsKey(string key)
        {
            return _preferences.ContainsKey(key);
        }

        public T Get<T>(string key, T defaultValue)
        {
            return _preferences.Get(key, defaultValue);
        }

        public void Set<T>(string key, T value)
        {
            _preferences.Set(key, value);
        }
    }
}
