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

        public BluetoothNotifier(IBleManager bleManager)
        {
            _bleManager = bleManager;
        }

        public partial bool HasConnection();

        public partial Task Connect();

        public partial void Disconnect();

        public partial void Dispose();
    }
}
