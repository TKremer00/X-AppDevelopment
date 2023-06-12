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

        private bool FilterSearchPlant(ObservablePlant observablePlant)
        {
            if (string.IsNullOrEmpty(SearchPlantName))
            {
                return true;
            }

            return observablePlant.Plant.PlantName.Contains(SearchPlantName, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
