using FinalProject.Data.Interfaces;
using FinalProject.Data.Models;

namespace FinalProject.Core.Helpers
{
    public class PreferenceWrapper : IPreferencesWrapper
    {
        private readonly IPreferences _preferences;

        public PreferenceWrapper(IPreferences preferences)
        {
            _preferences = preferences;
        }

        public event EventHandler<PreferenceUpdate> SettingChanged;

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
            SettingChanged?.Invoke(this, new PreferenceUpdate { Key = key, NewValue = value, OldValue = Get<T>(key, default) });
            _preferences.Set(key, value);
        }
    }
}
