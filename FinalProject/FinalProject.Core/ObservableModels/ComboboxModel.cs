using CommunityToolkit.Mvvm.ComponentModel;

namespace FinalProject.Core.ObservableModels
{
    public class ComboboxModel : ObservableObject
    {
        private readonly string[] _values;
        private int _chosenIndex;

        public ComboboxModel(string[] values) : this(values, 0)
        {
        }

        public ComboboxModel(string[] values, int defaultIndex)
        {
            _values = values;
            _chosenIndex = defaultIndex;
        }

        public int ChosenIndex
        {
            get => _chosenIndex;
            set => SetProperty(ref _chosenIndex, value);
        }

        public string[] Values => _values;
    }
}
