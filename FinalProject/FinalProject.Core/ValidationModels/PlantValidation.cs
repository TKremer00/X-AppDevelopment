using FinalProject.Core.Enums;
using FinalProject.Core.Helpers;
using FinalProject.Core.ObservableModels;
using FinalProject.Persistence.Models;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Core.ValidationModels
{
    public class PlantValidation : BaseValidationModel<Plant>
    {
        private string _latinPlantName;
        private string _plantName;

        public PlantValidation()
        {
            var temperatureValues = Enum.GetValues<TemperatureRanges>()
                .Select(x =>
                {
                    var (min, max) = x.GetPreferredTemperatures();
                    return $"{min} till {max} {SettingsHelper.GetTemperatureUnitSetting().GetSymbol()}";
                }).ToArray();

            TemperatureRanges = new ComboboxModel(temperatureValues, 1);
            var humidityValues = Enum.GetNames<HumidityRanges>().Select(x => x.Replace("_", " ").Trim() + "%").ToArray();

            HumidityRanges = new ComboboxModel(humidityValues, 4);
        }

        public string ImageUrl { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string LatinPlantName
        {
            get => _latinPlantName;
            set
            {
                SetProperty(ref _latinPlantName, value, true);
                OnPropertyChanged(nameof(LatinPlantNameValidationResult));
                OnPropertyChanged(nameof(HasLatinPlantNameError));
            }
        }

        public IEnumerable<ValidationResult> LatinPlantNameValidationResult => GetErrors(nameof(LatinPlantName));

        public bool HasLatinPlantNameError => LatinPlantNameValidationResult.Any();

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string PlantName
        {
            get => _plantName;
            set
            {
                SetProperty(ref _plantName, value, true);
                OnPropertyChanged(nameof(PlantNameValidationResult));
                OnPropertyChanged(nameof(HasPlantNameError));
            }
        }

        public IEnumerable<ValidationResult> PlantNameValidationResult => GetErrors(nameof(PlantName));

        public bool HasPlantNameError => PlantNameValidationResult.Any();

        public ComboboxModel TemperatureRanges { get; }

        public ComboboxModel HumidityRanges { get; }

        internal override Plant ConvertToModel()
        {
            var temperatureRange = Enum.GetValues<Enums.TemperatureRanges>()[TemperatureRanges.ChosenIndex];
            var (minTemperature, maxTemperature) = temperatureRange switch
            {
                Enums.TemperatureRanges._0_till_10 => (0, 10),
                Enums.TemperatureRanges._10_till_20 => (10, 20),
                Enums.TemperatureRanges._20_till_30 => (20, 30),
                _ => throw new NotImplementedException(),
            };

            var humidityRanges = Enum.GetValues<Enums.HumidityRanges>()[HumidityRanges.ChosenIndex];
            var (minHumidity, maxHumidity) = humidityRanges switch
            {
                Enums.HumidityRanges._0_till_10 => (0, 10),
                Enums.HumidityRanges._10_till_20 => (10, 20),
                Enums.HumidityRanges._20_till_30 => (20, 30),
                Enums.HumidityRanges._30_till_40 => (30, 40),
                Enums.HumidityRanges._40_till_50 => (40, 50),
                Enums.HumidityRanges._50_till_60 => (50, 60),
                Enums.HumidityRanges._60_till_70 => (60, 70),
                Enums.HumidityRanges._70_till_80 => (70, 80),
                Enums.HumidityRanges._80_till_90 => (80, 90),
                Enums.HumidityRanges._90_till_100 => (90, 100),
                _ => throw new NotImplementedException(),
            };

            return new Plant
            {
                LatinPlantName = LatinPlantName,
                PlantName = PlantName,
                MinTemperature = minTemperature,
                MaxTemperature = maxTemperature,
                MinHumidity = minHumidity,
                MaxHumidity = maxHumidity,
                ImageUrl = ImageUrl,
            };
        }
    }
}
