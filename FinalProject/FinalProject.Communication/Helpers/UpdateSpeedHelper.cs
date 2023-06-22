using FinalProject.Data.Enums;
using FinalProject.Data.Extensions;

namespace FinalProject.Communication.Helpers
{
    internal static class UpdateSpeedHelper
    {
        public static int GetUpdateSpeedInSeconds()
        {
            return ((UpdateEnviromentSpeeds)Preferences.Default.Get(Settings.UpdateEnviromentSpeed.ToString(), 0)).GetSecconds();
        }
    }
}
