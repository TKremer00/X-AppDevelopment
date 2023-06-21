using FinalProject.Communication.Data.Enums;
using FinalProject.Communication.Data.Models;
using Characteristics = FinalProject.Communication.Data.Enums.Characteristics;

namespace FinalProject.Communication.Communication
{
    partial class BluetoothNotifier
    {
        private Thread _thread;
        private Random _random;
        private Characteristics[] _characteristics;
        private bool _isDisposing;
        private bool _isRunning;

        partial void Ctor()
        {
            _characteristics = Enum.GetValues<Characteristics>();
            _thread = new Thread(SimulateDataUpdate);
            _random = new Random();
        }

        public partial bool HasConnection() => _isRunning;

        public partial async Task Connect()
        {
            if (!_thread.IsAlive)
            {
                _thread.Start();
            }

            StateChanged.Invoke(this, BluetoothStates.Connecting);
            // Simulate a connection
            await Task.Delay(250);
            StateChanged.Invoke(this, BluetoothStates.Connected);
            _isRunning = true;
        }

        public partial void Disconnect()
        {
            _isRunning = false;
        }

        private void SimulateDataUpdate()
        {
            while (!_isDisposing)
            {
                while (_isRunning)
                {
                    foreach (var characteristic in _characteristics)
                    {
                        if (characteristic == Characteristics.Pressure)
                        {
                            SensorDataChanged?.Invoke(this, new SensorData(characteristic, GenerateData(characteristic), (raw) => raw.FirstOrDefault() * 10));
                            continue;
                        }

                        SensorDataChanged?.Invoke(this, new SensorData(characteristic, GenerateData(characteristic)));
                    }
                    Thread.Sleep(250);
                }

                Thread.Sleep(500);
            }
        }

        private byte[] GenerateData(Characteristics characteristic)
        {
            return characteristic switch
            {
                Characteristics.Temperature => new byte[] { (byte)(_random.Next(32) + 15), 0, 0, 0 },
                Characteristics.Pressure => new byte[] { (byte)(_random.Next(150) + 90), 0, 0, 0 },
                Characteristics.Humidity => new byte[] { (byte)(_random.Next(65) + 35), 0, 0, 0 },
                Characteristics.IndoorAirQuality => new byte[] { (byte)(_random.Next(115) + 15), 0, 0, 0 },
                Characteristics.BatteryVoltage => new byte[] { 200, 0, 0, 0 },
                _ => throw new NotImplementedException(),
            };
        }

        public partial void Dispose()
        {
            _isDisposing = true;
            Disconnect();
        }
    }
}
