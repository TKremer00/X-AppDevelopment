using FinalProject.Core.Enums;
using FinalProject.Core.Helpers;

namespace FinalProject.Core.ObservableModels
{
    public class SettingModel : ComboboxModel
    {
        private readonly Settings _setting;

        public SettingModel(Settings setting, string[] values) : base(values, SettingsHelper.GetSetting(setting))
        {
            _setting = setting;
        }

        internal Settings Setting => _setting;
    }
}
