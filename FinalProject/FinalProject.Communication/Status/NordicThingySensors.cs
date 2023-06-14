namespace FinalProject.Communication.Status
{
    internal readonly struct NordicThingySensors
    {
        private static readonly Guid _temperatureGuid = new("06a55c4-b5e7-46fa-8326-8acaeb1189eb");
        private static readonly Guid _pressureGuid = new("51838aff-2d9a-b32a-b32a-8187e41664ba");
        private static readonly Guid _humidityGuid = new("753e3050-df06-4b53-b090-5e1d810c4383");
        private static readonly Guid _airqualityGuid = new("4603c9e5-ff29-412b-8e57-4b900491d68c");
        private static readonly Guid _redColorGuid = new("82754bbb-6ed3-4d69-a0e1-f19f6b654ec2");
        private static readonly Guid _greenColorGuid = new("db7f9f36-92ce-4509-a2ef-af72ba38fb48");
        private static readonly Guid _blueColorGuid = new("f5d2eab5-41e8-4f7c-aef7-c9fff4c544c0");
        private static readonly Guid _batteryGuid = new("fa3cf070-d0c7-4668-96c4-86125c8ac5df");

        private static Guid TemperatureSensor => _temperatureGuid;

        private static Guid PressureSensor => _pressureGuid;

        private static Guid HumiditySensor => _humidityGuid;

        private static Guid AirqualityGuid => _airqualityGuid;

        private static Guid RedColorSensor => _redColorGuid;

        private static Guid GreenColorSensor => _greenColorGuid;

        private static Guid BlueColorSensor => _blueColorGuid;

        private static Guid BatterySensor => _batteryGuid;
    }
}
