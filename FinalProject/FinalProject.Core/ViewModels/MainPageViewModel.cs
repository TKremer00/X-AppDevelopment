using AlohaKit.Models;
using CommunityToolkit.Mvvm.Input;
using FinalProject.Core.Enums;
using FinalProject.Core.Helpers;
using FinalProject.Persistence.Models;
using System.Collections.ObjectModel;

namespace FinalProject.Core.ViewModels
{
    public class MainPageViewModel : BaseViewModel
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

            SettingsCommand = new AsyncRelayCommand(HandleSettingsCommandAsync);
        }

        public ObservableCollection<ChartItem> Temperatures { get; }

        public ObservableCollection<Plant> Plants { get; }

        public AsyncRelayCommand SettingsCommand { get; }


        private async Task HandleSettingsCommandAsync()
        {
            await RoutingHelper.NavigateToAsync(Routes.SettingsPage);
        }
    }
}
