using CommunityToolkit.Mvvm.Input;
using FinalProject.Core.Helpers;
using FinalProject.Core.Services;
using FinalProject.Core.ValidationModels;

namespace FinalProject.Core.ViewModels
{
    public class AddPlantPageViewModel : BaseViewModel
    {
        private readonly PlantService _service;
        private bool _isLoading;

        public AddPlantPageViewModel(PlantService plantService)
        {
            _service = plantService;
            SavePlantCommand = new AsyncRelayCommand(HandleSavePlantCommandAsync);
            Plant = new PlantValidation();
        }

        public PlantValidation Plant { get; private set; }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public AsyncRelayCommand SavePlantCommand { get; }

        private async Task HandleSavePlantCommandAsync()
        {
            if (Plant.HasErrors)
            {
                return;
            }

            IsLoading = true;
            var imageUrl = await RequestHelper.GetPlantImageAsync(Plant.LatinPlantName);
            Plant.ImageUrl = imageUrl;

            await _service.SavePlant(Plant);

            IsLoading = false;

            await ToasterHelper.Show($"Saved {Plant.PlantName} to database");

            // clear the old plant data
            Plant = new PlantValidation();
            OnPropertyChanged(nameof(Plant));

            await RoutingHelper.NavigateBackAsync();
        }

    }
}
