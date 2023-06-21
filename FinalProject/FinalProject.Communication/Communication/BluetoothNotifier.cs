using FinalProject.Data.Enums;
using FinalProject.Data.Models;
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
        partial void Ctor()
        {
            throw new NotImplementedException();
        }

        public partial bool HasConnection() => throw new NotImplementedException();

        public partial Task Connect()
        {
            throw new NotImplementedException();
        }

        public partial void Disconnect()
        {
            throw new NotImplementedException();
        }

        public partial void Dispose()
        {
            throw new NotImplementedException();
        }
    }
#endif
}
