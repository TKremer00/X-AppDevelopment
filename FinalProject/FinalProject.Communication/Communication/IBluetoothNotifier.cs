using FinalProject.Data.Enums;
using FinalProject.Data.Models;

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
