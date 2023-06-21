using FinalProject.Communication.Data.Enums;
using FinalProject.Communication.Data.Models;
using Shiny.BluetoothLE;
using CharacteristicExtensions = FinalProject.Communication.Data.Enums.CharacteristicExtensions;

namespace FinalProject.Communication.Communication
{
    partial class BluetoothNotifier
    {
        private IPeripheral _peripheral;

        public partial bool HasConnection() => _peripheral?.IsConnected() ?? false;

        partial void Ctor()
        {
        }

        public partial async Task Connect()
        {
            const string NORDIC_THINGY_UUID = "00000000-0000-0000-0000-ca5d92b32ecd";
            const string PAIRING_CODE = "123456";
            try
            {
                if (_bleManager.IsScanning)
                {
                    return;
                }

                var scanner = _bleManager.CreateManagedScanner();

                try
                {
                    await scanner.Start(predicate: scanResult => scanResult.Peripheral.Uuid == NORDIC_THINGY_UUID);
                }
                catch (Java.Lang.NullPointerException)
                {
                    StateChanged?.Invoke(this, BluetoothStates.BluetoothNotEnabled);
                    return;
                }

                StateChanged.Invoke(this, BluetoothStates.Connecting);
                await Task.Delay(TimeSpan.FromSeconds(10));

                var resultedPeripheral = scanner.Peripherals.FirstOrDefault();
                if (resultedPeripheral == null)
                {
                    StateChanged?.Invoke(this, BluetoothStates.NoAvailableDevices);
                    return;
                }

                _peripheral = resultedPeripheral.Peripheral;
                await _peripheral.ConnectAsync(timeout: TimeSpan.FromSeconds(30));
                var isPaired = _peripheral.TryPairingRequest(PAIRING_CODE);

                isPaired.Subscribe(Paired);
                scanner.Stop();
            }
            catch (Exception e)
            {
                var test = e;
            }
        }

        public partial void Disconnect()
        {
            StateChanged?.Invoke(this, BluetoothStates.Connect);
            _peripheral.CancelConnection();
        }

        private void Paired(bool? isPaired)
        {
            const string SERVICE_UUID = "a5b46352-9d13-479f-9fcb-3dcdf0a13f4d";
            if (!isPaired.HasValue || isPaired == false)
            {
                StateChanged?.Invoke(this, BluetoothStates.PairingFailed);
                return;
            }
            StateChanged.Invoke(this, BluetoothStates.Connected);

            var characteristics = Enum.GetValues<Characteristics>();
            foreach (var characteristic in characteristics)
            {
                var sensorCharacteristic = _peripheral.NotifyCharacteristic(SERVICE_UUID, characteristic.GetUUID());
                sensorCharacteristic.SubscribeAsync(SubscribeSensorCharacteristic);
            }
        }

        private Task SubscribeSensorCharacteristic(BleCharacteristicResult arg)
        {
            var data = arg.Data;
            var characteristic = arg.Characteristic.Uuid switch
            {
                CharacteristicExtensions.TEMP_UUID => Characteristics.Temperature,
                CharacteristicExtensions.PRESSURE_UUID => Characteristics.Pressure,
                CharacteristicExtensions.HUMIDITY_UUID => Characteristics.Humidity,
                CharacteristicExtensions.INDOOR_AIR_QUALITY_UUID => Characteristics.IndoorAirQuality,
                CharacteristicExtensions.BATTERY_VOLTAGE_UUID => Characteristics.BatteryVoltage,
                _ => throw new NotImplementedException(),
            };

            if (characteristic == Characteristics.Pressure)
            {
                SensorDataChanged?.Invoke(this, new SensorData(characteristic, data, (raw) => raw.FirstOrDefault() * 10));
                return Task.CompletedTask;
            }

            SensorDataChanged.Invoke(this, new SensorData(characteristic, data));

            return Task.CompletedTask;
        }

        public partial void Dispose()
        {
            if (HasConnection())
            {
                Disconnect();
            }
        }
    }
}
