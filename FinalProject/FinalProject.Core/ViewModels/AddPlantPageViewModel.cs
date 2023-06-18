using CommunityToolkit.Mvvm.Input;
using FinalProject.Core.Helpers;
using FinalProject.Core.Services;
using FinalProject.Core.ValidationModels;

namespace FinalProject.Core.ViewModels
{
    public class AddPlantPageViewModel : BaseViewModel
    {
        private readonly PlantService _service;

        public AddPlantPageViewModel(PlantService plantService)
        {
            _service = plantService;
            SavePlantCommand = new AsyncRelayCommand(HandleSavePlantCommandAsync);
            Plant = new PlantValidation();
        }

        public PlantValidation Plant { get; }

        public AsyncRelayCommand SavePlantCommand { get; }

        private async Task HandleSavePlantCommandAsync()
        {
            // TODO: check if needs spinner because of api request.
            if (Plant.HasErrors)
            {
                return;
            }

            var imageUrl = await RequestHelper.GetPlantImage(Plant.LatinPlantName);
            Plant.ImageUrl = imageUrl;

            await _service.SavePlant(Plant);

            await ToasterHelper.Show($"Save {Plant.PlantName} to database");

            await RoutingHelper.NavigateBackAsync();
        }

    }
}
