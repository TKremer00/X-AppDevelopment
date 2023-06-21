namespace FinalProject.Data.Enums
{
    public enum Characteristics
    {
        Temperature,
        Pressure,
        Humidity,
        IndoorAirQuality,
        BatteryVoltage
    }

    public static class CharacteristicExtensions
    {
        public const string TEMP_UUID = "506a55c4-b5e7-46fa-8326-8acaeb1189eb";
        public const string PRESSURE_UUID = "51838aff-2d9a-b32a-b32a-8187e41664ba";
        public const string HUMIDITY_UUID = "753e3050-df06-4b53-b090-5e1d810c4383";
        public const string INDOOR_AIR_QUALITY_UUID = "4603c9e5-ff29-412b-8e57-4b900491d68c";
        public const string BATTERY_VOLTAGE_UUID = "fa3cf070-d0c7-4668-96c4-86125c8ac5df";

        public static string GetUUID(this Characteristics characteristics)
        {
            return characteristics switch
            {
                Characteristics.Temperature => TEMP_UUID,
                Characteristics.Pressure => PRESSURE_UUID,
                Characteristics.Humidity => HUMIDITY_UUID,
                Characteristics.IndoorAirQuality => INDOOR_AIR_QUALITY_UUID,
                Characteristics.BatteryVoltage => BATTERY_VOLTAGE_UUID,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
