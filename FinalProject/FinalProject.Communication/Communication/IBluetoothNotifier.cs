using FinalProject.Communication.Data.Enums;
using FinalProject.Communication.Data.Models;

namespace FinalProject.Communication.Communication
{
    public interface IBluetoothNotifier : IDisposable
    {
        event EventHandler<BluetoothStates> StateChanged;
        event EventHandler<SensorData> SensorDataChanged;

        public bool HasConnection();

        public Task Connect();

        public void Disconnect();
    }
}
