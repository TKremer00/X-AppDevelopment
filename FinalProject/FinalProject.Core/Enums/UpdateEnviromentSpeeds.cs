namespace FinalProject.Core.Enums
{
    public enum UpdateEnviromentSpeeds
    {
        SECONDS_10,
        SECONDS_30,
        SECONDS_60,
        SECONDS_90,
        SECONDS_120,
        SECONDS_180,
        SECONDS_240,
    }

    public static class UpdateEnviromentSpeedsExtensions
    {
        public static int GetSecconds(this UpdateEnviromentSpeeds updateEnviromentSpeed)
        {
            return updateEnviromentSpeed switch
            {
                UpdateEnviromentSpeeds.SECONDS_10 => 10,
                UpdateEnviromentSpeeds.SECONDS_30 => 30,
                UpdateEnviromentSpeeds.SECONDS_60 => 60,
                UpdateEnviromentSpeeds.SECONDS_90 => 90,
                UpdateEnviromentSpeeds.SECONDS_120 => 120,
                UpdateEnviromentSpeeds.SECONDS_180 => 180,
                UpdateEnviromentSpeeds.SECONDS_240 => 240,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
