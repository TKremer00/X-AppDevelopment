using FinalProject.Data.Models;

namespace FinalProject.Data.Interfaces
{
    public interface IPreferencesWrapper
    {
        public event EventHandler<PreferenceUpdate> SettingChanged;

        public T Get<T>(string key, T defaultValue);

        void Set<T>(string key, T value);
    }
}
