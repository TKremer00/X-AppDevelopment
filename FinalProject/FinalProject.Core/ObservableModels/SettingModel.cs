using FinalProject.Core.Extensions;
using FinalProject.Data.Enums;

namespace FinalProject.Core.ObservableModels
{
    public class SettingModel : ComboboxModel
    {
        private readonly Settings _setting;

        public SettingModel(Settings setting, string[] values) : base(values, Preferences.Default.GetSetting(setting))
        {
            _setting = setting;
        }

        internal Settings Setting => _setting;
    }
}
