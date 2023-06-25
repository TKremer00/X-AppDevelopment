using AlohaKit.Models;
using CommunityToolkit.Mvvm.Input;
using FinalProject.Communication.Communication;
using FinalProject.Core.Converters;
using FinalProject.Core.Extensions;
using FinalProject.Core.Helpers;
using FinalProject.Core.ObservableModels;
using FinalProject.Core.Services;
using FinalProject.Data.Enums;
using FinalProject.Data.Interfaces;
using FinalProject.Data.Models;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace FinalProject.Core.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly object _updatePlantsLock;
        private readonly object _updateTemperaturesLock;
        private const int MAX_TEMPERATURES = 10;
        private readonly PlantService _plantService;
        private readonly TemperatureService _temperatureService;
        private readonly IBluetoothNotifier _bluetoothNotifier;
        private int? _humidity;
        private int? _pressure;
        private int? _indoorAirQuality;
        private ObservableCollection<ChartItem> _temperatures;
        private readonly TemperatureConverter _temperatureConverter;


        public MainPageViewModel(PlantService plantService, TemperatureService temperatureService, TemperatureConverter temperatureConverter, IBluetoothNotifier bluetoothNotifier, IPreferencesWrapper preferences)
        {
            _plantService = plantService;
            _temperatureService = temperatureService;
            _temperatureConverter = temperatureConverter;
            _bluetoothNotifier = bluetoothNotifier;
            _updatePlantsLock = new object();
            _updateTemperaturesLock = new object();

            _temperatures = new();

            _ = UpdatePlants();
            _ = UpdateTemperature();

            _bluetoothNotifier.StateChanged += BluetoothNotifierStateChanged;
            _bluetoothNotifier.SensorDataChanged += SensorDataChanged;
            RoutingHelper.RoutingHelperNavigationChanged += RoutingHelperNavigationChanged;
            preferences.SettingChanged += PreferencesSettingChanged;

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

            lock (_updatePlantsLock)
            {
                if (Plants?.FirstOrDefault()?.Plant.Id == plants.FirstOrDefault()?.Plant.Id)
                {
                    return;
                }

                Plants = plants;
                OnPropertyChanged(nameof(Plants));

            }
        }

        public async Task UpdateTemperature()
        {
            var temperatures = await _temperatureService.GetLastTemperaturesAsync(10);

            lock (_updateTemperaturesLock)
            {
                var charts = temperatures
                    .Select(x => new ChartItem { Value = _temperatureConverter.Convert(x.IntValue), Label = $"{x.LocalTime:HH:mm:ss}" })
                    .ToList();

                foreach (var chartItem in charts)
                {
                    _temperatures.AddAndRemoveFirst(MAX_TEMPERATURES, chartItem);
                }
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

        private void PreferencesSettingChanged(object sender, PreferenceUpdate e)
        {
            if (e.Key != Settings.TemperatureUnit.ToString())
            {
                return;
            }

            lock (_updateTemperaturesLock)
            {
                foreach (var chartItem in _temperatures)
                {
                    var celsius = (int)_temperatureConverter.ConvertBack(chartItem, null, () => (TemperatureUnits)e.OldValue, null);
                    chartItem.Value = _temperatureConverter.Convert(celsius);
                }
            }
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
                            Value = _temperatureConverter.Convert(e.Value),
                            Label = $"{e.LocalTime:HH:mm:ss}"
                        };

                        lock (_updateTemperaturesLock)
                        {
                            _temperatures.AddAndRemoveFirst(MAX_TEMPERATURES, chartItem);
                        }

                        lock (_updatePlantsLock)
                        {
                            Plants.UpdateAmbient((p, v) => p.UpdateTemperature(v), e.Value);
                        }

                        break;
                    case Characteristics.Pressure:
                        Pressure = e.Value;
                        break;
                    case Characteristics.Humidity:
                        Humidity = e.Value;

                        lock (_updatePlantsLock)
                        {
                            Plants.UpdateAmbient((p, v) => p.UpdateHumidity(v), e.Value);
                        }
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
            if (!_bluetoothNotifier.HasConnection())
            {
                await _bluetoothNotifier.Connect();
            }
            else
            {
                _bluetoothNotifier.Disconnect();
            }
        }

        private async Task HandleGoToPlantsCommand()
        {
            await RoutingHelper.NavigateToAsync(Routes.PlantsPage);
        }
    }
}
