using CommunityToolkit.Mvvm.ComponentModel;
using FinalProject.Core.Enums;
using FinalProject.Core.Helpers;

namespace FinalProject.Core.ObservableModels
{
    public class SettingModel : ObservableObject
    {
        private readonly string[] _values;
        private readonly Settings _setting;
        private int _chosenIndex;


        public SettingModel(Settings setting, string[] values)
        {
            _setting = setting;
            _values = values;
            _chosenIndex = SettingsHelper.GetSetting(_setting);
        }

        public int ChosenIndex
        {
            get => _chosenIndex;
            set => SetProperty(ref _chosenIndex, value);
        }

        internal Settings Setting => _setting;

        public string[] Values => _values;
    }
}
