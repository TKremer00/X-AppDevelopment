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

        public PlantValidation Plant { get; }

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
            var imageUrl = await RequestHelper.GetPlantImage(Plant.LatinPlantName);
            Plant.ImageUrl = imageUrl;

            await _service.SavePlant(Plant);

            IsLoading = false;

            await ToasterHelper.Show($"Save {Plant.PlantName} to database");

            await RoutingHelper.NavigateBackAsync();
        }

    }
}
