﻿using FinalProject.Communication.Data.Enums;
using FinalProject.Communication.Data.Models;
using Shiny.BluetoothLE;

namespace FinalProject.Communication.Communication
{
    public partial class BluetoothNotifier : IBluetoothNotifier
    {
        public event EventHandler<SensorData> SensorDataChanged;
        public event EventHandler<BluetoothStates> StateChanged;

        private readonly IBleManager _bleManager;

        public BluetoothNotifier(IBleManager bleManager)
        {
            _bleManager = bleManager;
            Ctor();
        }

        partial void Ctor();

        public partial bool HasConnection();

        public partial Task Connect();

        public partial void Disconnect();

        public partial void Dispose();
    }

    // NOTE: this is to target net7.0
#if !ANDROID && !WINDOWS
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
            if (_thread.IsAlive)
            {
                return;
            }

            StateChanged.Invoke(this, BluetoothStates.Connecting);

            // Simulate a connection
            await Task.Delay(250);
            _thread.Start();
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
                        SensorDataChanged?.Invoke(this, new SensorData(characteristic, GenerateData(characteristic)));
                    }
                    Thread.Sleep(250);
                }

                Thread.Sleep(500);
            }
        }

        public partial void Dispose()
        {
            _isDisposing = true;
        }

        private byte[] GenerateData(Characteristics characteristic)
        {
            return characteristic switch
            {
                Characteristics.Temperature => new byte[] { (byte)(_random.Next(32) + 15), 0, 0, 0 },
                Characteristics.Pressure => new byte[] { (byte)(_random.Next(32) + 15), 0, 0, 0 }, // TODO: correct this value.
                Characteristics.Humidity => new byte[] { (byte)(_random.Next(65) + 35), 0, 0, 0 },
                Characteristics.IndoorAirQuality => new byte[] { (byte)(_random.Next(115) + 15), 0, 0, 0 },
                Characteristics.BatteryVoltage => new byte[] { 200, 0, 0, 0 },
                _ => throw new NotImplementedException(),
            };
        }
    }
#endif
}
