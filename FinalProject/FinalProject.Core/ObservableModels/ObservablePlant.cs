using CommunityToolkit.Mvvm.ComponentModel;
using FinalProject.Persistence.Models;

namespace FinalProject.Core.ObservableModels
{
    public class ObservablePlant : ObservableObject
    {
        private Color _backgroundColor;

        public ObservablePlant(Plant plant)
        {
            Plant = plant;
            _backgroundColor = Colors.Transparent;
        }

        public Plant Plant { get; }

        public Color BackgroundColor
        {
            get => _backgroundColor;
            set => SetProperty(ref _backgroundColor, value);
        }
    }
}
