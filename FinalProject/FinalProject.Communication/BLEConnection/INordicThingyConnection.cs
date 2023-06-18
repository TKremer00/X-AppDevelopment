using FinalProject.Communication.Status;
using Shiny.BluetoothLE;

namespace FinalProject.Communication.BLEConnection
{
    internal interface INordicThingyConnection : IBleDelegate
    {
        Task ConnectAsync(Action<ConnectionStatus> statusUpdateCallback);
    }
}
