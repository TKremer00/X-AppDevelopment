using AlohaKit.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using FinalProject.Persistence.Models;
using System.Collections.ObjectModel;

namespace FinalProject.Core.ViewModels
{
    public class MainPageViewModel : ObservableObject
    {
        public MainPageViewModel()
        {
            Temperatures = new()
            {
                new ChartItem { Value = 23, Label = "Value 1", },
                new ChartItem { Value = 25, Label = "Value 2", },
                new ChartItem { Value = 30, Label = "Value 3", },
                new ChartItem { Value = 10, Label = "Value 4", },
                new ChartItem { Value = 22, Label = "Value 5", }
            };

            Plants = new()
            {
                new Plant { PlantName = "Orchidee", ImageUrl = "https://www.optiflor.nl/static/site/header-circle-plus-flower.png" },
                new Plant { PlantName = "Orchidee2", ImageUrl = "https://www.optiflor.nl/static/site/header-circle-plus-flower.png" },
            };
        }

        public ObservableCollection<ChartItem> Temperatures { get; }

        public ObservableCollection<Plant> Plants { get; }
    }
}
