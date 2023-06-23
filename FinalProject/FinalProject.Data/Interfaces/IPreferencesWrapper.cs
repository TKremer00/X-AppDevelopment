namespace FinalProject.Data.Interfaces
{
    public interface IPreferencesWrapper
    {
        public T Get<T>(string key, T defaultValue);

        void Set<T>(string key, T value);
    }
}
