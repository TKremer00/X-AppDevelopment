using CommunityToolkit.Mvvm.Input;
using FinalProject.Core.Enums;
using FinalProject.Core.Helpers;
using FinalProject.Core.ObservableModels;
using FinalProject.Persistence.Models;

namespace FinalProject.Core.ViewModels
{
    public class PlantsPageViewModel : BaseViewModel
    {
        private string _searchPlantName;
        private List<Plant> _plants;

        public PlantsPageViewModel()
        {
            _plants = new() {
                new Plant { PlantName = "Orchidee", ImageUrl = "https://www.optiflor.nl/static/site/header-circle-plus-flower.png" },
                new Plant { PlantName = "Orchidee2", ImageUrl = "https://www.optiflor.nl/static/site/header-circle-plus-flower.png" },
            };

            GoToAddPlant = new AsyncRelayCommand(HandleGoToAddPlantAsync);
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
