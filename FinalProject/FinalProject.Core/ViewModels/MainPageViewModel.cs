using AlohaKit.Models;
using CommunityToolkit.Mvvm.Input;
using FinalProject.Core.Enums;
using FinalProject.Core.Helpers;
using FinalProject.Core.ObservableModels;
using FinalProject.Core.Services;
using System.Collections.ObjectModel;

namespace FinalProject.Core.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly PlantService _plantService;
        public MainPageViewModel(PlantService plantService)
        {
            _plantService = plantService;

            // TODO: get temperatures from database/thingy.
            Temperatures = new()
            {
                new ChartItem { Value = 23, Label = "Value 1", },
                new ChartItem { Value = 25, Label = "Value 2", },
                new ChartItem { Value = 30, Label = "Value 3", },
                new ChartItem { Value = 10, Label = "Value 4", },
                new ChartItem { Value = 22, Label = "Value 5", }
            };

            _ = UpdatePlants();

            RoutingHelper.RoutingHelperNavigationChanged += RoutingHelperNavigationChanged;

            GoToPlantsCommand = new AsyncRelayCommand(HandleGoToPlantsCommand);
            GoToSettingsCommand = new AsyncRelayCommand(HandleGoToSettingsCommandAsync);
        }

        public async Task UpdatePlants()
        {
            var mostRecentPlants = await _plantService.GetMostRecentAsync();
            var plants = mostRecentPlants.Select(x => new ObservablePlant(x)).ToList();

            if (Plants?.FirstOrDefault()?.Plant.Id == plants.FirstOrDefault()?.Plant.Id)
            {
                return;
            }

            Plants = plants;
            OnPropertyChanged(nameof(Plants));
        }

        private async void RoutingHelperNavigationChanged(object sender, RoutEventArgs e)
        {
            if (e.To != Routes.MainPage)
            {
                return;
            }

            await UpdatePlants();
        }

        public ObservableCollection<ChartItem> Temperatures { get; }

        public IEnumerable<ObservablePlant> Plants { get; private set; }

        public AsyncRelayCommand GoToPlantsCommand { get; }

        public AsyncRelayCommand GoToSettingsCommand { get; }

        private async Task HandleGoToSettingsCommandAsync()
        {
            await RoutingHelper.NavigateToAsync(Routes.SettingsPage);
        }

        private async Task HandleGoToPlantsCommand()
        {
            await RoutingHelper.NavigateToAsync(Routes.PlantsPage);
        }
    }
}
