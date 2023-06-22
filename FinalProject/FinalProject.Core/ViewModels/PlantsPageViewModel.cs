using CommunityToolkit.Mvvm.Input;
using FinalProject.Communication.Communication;
using FinalProject.Core.Extensions;
using FinalProject.Core.Helpers;
using FinalProject.Core.ObservableModels;
using FinalProject.Core.Services;
using FinalProject.Data.Enums;
using FinalProject.Data.Models;

namespace FinalProject.Core.ViewModels
{
    public class PlantsPageViewModel : BaseViewModel
    {
        private readonly object _updatePlantsLock;
        private string _searchPlantName;
        private readonly PlantService _service;
        private List<Plant> _plants;

        public PlantsPageViewModel(PlantService service, IBluetoothNotifier bluetoothNotifier)
        {
            _updatePlantsLock = new object();
            _service = service;
            _plants = new List<Plant>();
            bluetoothNotifier.SensorDataChanged += SensorDataChanged;
            _ = UpdatePlantsAsync();
            GoToAddPlant = new AsyncRelayCommand(HandleGoToAddPlantAsync);
            RoutingHelper.RoutingHelperNavigationChanged += NavigationChanged;
        }

        private async Task UpdatePlantsAsync()
        {
            var plants = await _service.GetPlantsAsync();

            lock (_updatePlantsLock)
            {
                if (_plants?.LastOrDefault()?.Id == plants.LastOrDefault()?.Id)
                {
                    return;
                }

                _plants = plants;
                OnPropertyChanged(nameof(Plants));
            }
        }

        public string SearchPlantName
        {
            get => _searchPlantName;
            set
            {
                SetProperty(ref _searchPlantName, value);
                OnPropertyChanged(nameof(Plants));
            }
        }

        public IEnumerable<ObservablePlant> Plants => _plants.Select(x => new ObservablePlant(x)).Where(FilterSearchPlant);

        public AsyncRelayCommand GoToAddPlant { get; }

        private async void SensorDataChanged(object sender, SensorData e)
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                if (e.Characteristic == Characteristics.Temperature)
                {
                    lock (_updatePlantsLock)
                    {
                        Plants.UpdateAmbient((p, v) => p.UpdateTemperature(v), e.Value);
                    }
                }
                else if (e.Characteristic == Characteristics.Humidity)
                {
                    lock (_updatePlantsLock)
                    {
                        Plants.UpdateAmbient((p, v) => p.UpdateHumidity(v), e.Value);
                    }

                }
            });
        }

        private async void NavigationChanged(object sender, RoutEventArgs e)
        {
            if (e.To != Routes.PlantsPage)
            {
                return;
            }

            await UpdatePlantsAsync();
        }

        private bool FilterSearchPlant(ObservablePlant observablePlant)
        {
            if (string.IsNullOrEmpty(SearchPlantName))
            {
                return true;
            }

            return observablePlant.Plant.PlantName.Contains(SearchPlantName, StringComparison.InvariantCultureIgnoreCase);
        }

        private async Task HandleGoToAddPlantAsync()
        {
            await RoutingHelper.NavigateToAsync(Routes.AddPlantPage);
        }
    }
}
