using AlohaKit.Models;
using CommunityToolkit.Mvvm.Input;
using FinalProject.Communication.Communication;
using FinalProject.Core.Converters;
using FinalProject.Core.Extensions;
using FinalProject.Core.Helpers;
using FinalProject.Core.ObservableModels;
using FinalProject.Core.Services;
using FinalProject.Data.Enums;
using FinalProject.Data.Models;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace FinalProject.Core.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private const int MAX_TEMPERATURES = 10;

        private readonly PlantService _plantService;
        private readonly TemperatureService _temperatureService;
        private readonly IBluetoothNotifier _bluetoothNotifier;
        private int? _humidity;
        private int? _pressure;
        private int? _indoorAirQuality;
        private ObservableCollection<ChartItem> _temperatures;

        public MainPageViewModel(PlantService plantService, TemperatureService temperatureService, IBluetoothNotifier bluetoothNotifier)
        {
            _plantService = plantService;
            _temperatureService = temperatureService;
            _bluetoothNotifier = bluetoothNotifier;

            _temperatures = new();

            _ = UpdatePlants();
            _ = UpdateTemperature();

            _bluetoothNotifier.StateChanged += BluetoothNotifierStateChanged;
            _bluetoothNotifier.SensorDataChanged += SensorDataChanged;
            RoutingHelper.RoutingHelperNavigationChanged += RoutingHelperNavigationChanged;

            BluetoothConnectionText = BluetoothStates.Connect;
            GoToPlantsCommand = new AsyncRelayCommand(HandleGoToPlantsCommand);
            BluetoothCommand = new AsyncRelayCommand(HandleBluetoothCommand);
            GoToSettingsCommand = new AsyncRelayCommand(HandleGoToSettingsCommandAsync);
        }

        public BluetoothStates BluetoothConnectionText { get; private set; }

        public ObservableCollection<ChartItem> Temperatures => _temperatures;

        public IEnumerable<ObservablePlant> Plants { get; private set; }

        public AsyncRelayCommand GoToPlantsCommand { get; }

        public AsyncRelayCommand BluetoothCommand { get; }

        public AsyncRelayCommand GoToSettingsCommand { get; }

        public int? Humidity
        {
            get => _humidity;
            set => SetProperty(ref _humidity, value);
        }

        public int? Pressure
        {
            get => _pressure;
            set => SetProperty(ref _pressure, value);
        }

        public int? IndoorAirQuality
        {
            get => _indoorAirQuality;
            set => SetProperty(ref _indoorAirQuality, value);
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

        public async Task UpdateTemperature()
        {
            var temperatures = await _temperatureService.GetLastTemperaturesAsync(10);
            var charts = temperatures
                .Select(x => new ChartItem { Value = TemperatureConverter.temperatureConverter.Convert(x.IntValue), Label = $"{x.LocalTime:HH:mm:ss}" })
                .ToList();

            foreach (var chartItem in charts)
            {
                _temperatures.AddAndRemoveFirst(MAX_TEMPERATURES, chartItem);
            }
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

        private async void SensorDataChanged(object sender, SensorData e)
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                switch (e.Characteristic)
                {
                    case Characteristics.Temperature:
                        var chartItem = new ChartItem()
                        {
                            Value = TemperatureConverter.temperatureConverter.Convert(e.Value),
                            Label = $"{e.LocalTime:HH:mm:ss}"
                        };
                        _temperatures.AddAndRemoveFirst(MAX_TEMPERATURES, chartItem);
                        break;
                    case Characteristics.Pressure:
                        // TODO: fix with real data.
                        Pressure = e.Value;
                        break;
                    case Characteristics.Humidity:
                        Humidity = e.Value;
                        break;
                    case Characteristics.IndoorAirQuality:
                        IndoorAirQuality = e.Value;
                        break;
                }
            });
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
