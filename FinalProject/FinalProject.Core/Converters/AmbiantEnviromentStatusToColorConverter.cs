using FinalProject.Core.Extensions;
using FinalProject.Data.Enums;
using System.Globalization;

namespace FinalProject.Core.Converters
{
    public class AmbiantEnviromentStatusToColorConverter : BaseValueConverter<PlantAmbientEnviroment, Color>
    {
        private readonly Lazy<Color> _GoodColor = new(() => Application.Current.Resources.FindValueRecursive<Color>("PrimaryColor"));
        private readonly Lazy<Color> _PartialColor = new(() => Application.Current.Resources.FindValueRecursive<Color>("TertiaryColor"));

        public override Color Convert(PlantAmbientEnviroment value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                PlantAmbientEnviroment.Good => _GoodColor.Value,
                PlantAmbientEnviroment.Partial => _PartialColor.Value,
                PlantAmbientEnviroment.Bad => Colors.Transparent,
                _ => throw new NotImplementedException(),
            };
        }

        public override PlantAmbientEnviroment ConvertBack(Color value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
