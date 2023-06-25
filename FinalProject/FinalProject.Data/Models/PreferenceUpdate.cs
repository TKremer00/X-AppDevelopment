namespace FinalProject.Data.Models
{
    public class PreferenceUpdate
    {
        public string Key { get; set; } = null!;

        public object NewValue { get; set; } = null!;

        public object OldValue { get; set; } = null!;
    }
}
