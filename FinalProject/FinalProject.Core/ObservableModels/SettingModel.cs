using FinalProject.Core.Extensions;
using FinalProject.Data.Enums;
using FinalProject.Data.Interfaces;

namespace FinalProject.Core.ObservableModels
{
    public class SettingModel : ComboboxModel
    {
        private readonly Settings _setting;

        public SettingModel(Settings setting, string[] values) : base(values, Application.Current.GetRequiredService<IPreferencesWrapper>().GetSetting(setting))
        {
            _setting = setting;
        }

        internal Settings Setting => _setting;
    }
}
