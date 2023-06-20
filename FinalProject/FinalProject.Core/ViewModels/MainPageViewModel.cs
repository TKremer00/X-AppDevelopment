using AlohaKit.Models;
using CommunityToolkit.Mvvm.Input;
using FinalProject.Communication.Communication;
using FinalProject.Communication.Data.Enums;
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
        private readonly IBluetoothNotifier _bluetoothNotifier;

        public MainPageViewModel(PlantService plantService, IBluetoothNotifier bluetoothNotifier)
        {
            _plantService = plantService;
            _bluetoothNotifier = bluetoothNotifier;

            // TODO: get temperatures from database event on add.
            Temperatures = new()
            {
                new ChartItem { Value = 23, Label = "Value 1", },
                new ChartItem { Value = 25, Label = "Value 2", },
                new ChartItem { Value = 30, Label = "Value 3", },
                new ChartItem { Value = 10, Label = "Value 4", },
                new ChartItem { Value = 22, Label = "Value 5", }
            };

            _ = UpdatePlants();

            _bluetoothNotifier.StateChanged += BluetoothNotifierStateChanged;
            RoutingHelper.RoutingHelperNavigationChanged += RoutingHelperNavigationChanged;

            BluetoothConnectionText = BluetoothStates.Connect;
            GoToPlantsCommand = new AsyncRelayCommand(HandleGoToPlantsCommand);
            BluetoothCommand = new AsyncRelayCommand(HandleBluetoothCommand);
            GoToSettingsCommand = new AsyncRelayCommand(HandleGoToSettingsCommandAsync);
        }

        public BluetoothStates BluetoothConnectionText { get; private set; }

        public ObservableCollection<ChartItem> Temperatures { get; }

        public IEnumerable<ObservablePlant> Plants { get; private set; }

        public AsyncRelayCommand GoToPlantsCommand { get; }

        public AsyncRelayCommand BluetoothCommand { get; }

        public AsyncRelayCommand GoToSettingsCommand { get; }

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
        private void BluetoothNotifierStateChanged(object sender, BluetoothStates e)
        {
            BluetoothConnectionText = e;
            OnPropertyChanged(nameof(BluetoothConnectionText));
        }

        private async Task HandleGoToSettingsCommandAsync()
        {
            await RoutingHelper.NavigateToAsync(Routes.SettingsPage);
        }

        private async Task HandleBluetoothCommand()
        {
            await _bluetoothNotifier.Connect();
        }

        private async Task HandleGoToPlantsCommand()
        {
            await RoutingHelper.NavigateToAsync(Routes.PlantsPage);
        }
    }
}
