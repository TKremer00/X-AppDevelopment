using CommunityToolkit.Mvvm.Input;
using FinalProject.Core.Enums;
using FinalProject.Core.Helpers;
using FinalProject.Core.ObservableModels;
using FinalProject.Core.Services;
using FinalProject.Persistence.Models;

namespace FinalProject.Core.ViewModels
{
    public class PlantsPageViewModel : BaseViewModel
    {
        private string _searchPlantName;
        private readonly PlantService _service;
        private List<Plant> _plants;

        public PlantsPageViewModel(PlantService service)
        {
            _service = service;
            _ = UpdatePlantsAsync();
            GoToAddPlant = new AsyncRelayCommand(HandleGoToAddPlantAsync);
            RoutingHelper.RoutingHelperNavigationChanged += NavigationChanged;
        }

        private async Task UpdatePlantsAsync()
        {
            _plants = await _service.GetPlantsAsync();
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

        private async void NavigationChanged(object sender, RoutEventArgs e)
        {
            if (e.From != Routes.AddPlantPage && e.To != Routes.PlantsPage)
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
