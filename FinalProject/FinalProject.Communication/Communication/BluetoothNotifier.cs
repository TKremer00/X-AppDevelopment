using FinalProject.Communication.Data.Enums;
using FinalProject.Communication.Data.Models;
using Shiny.BluetoothLE;

namespace FinalProject.Communication.Communication
{
    public partial class BluetoothNotifier : IBluetoothNotifier
    {
        public event EventHandler<SensorData> SensorDataChanged;
        public event EventHandler<BluetoothStates> StateChanged;

        private readonly IBleManager _bleManager;
        private IPeripheral _peripheral;

        public BluetoothNotifier(IBleManager bleManager)
        {
            _bleManager = bleManager;
        }

        public virtual bool HasConnection => _peripheral?.IsConnected() ?? false;

        public partial Task Connect();

        public partial void Disconnect();

        public partial void Dispose();
    }
}
