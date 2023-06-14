using FinalProject.Communication.Status;

namespace FinalProject.Communication.BLEConnection
{
    internal interface INordicThingyConnection
    {
        Task ConnectAsync(Action<ConnectionStatus> statusUpdateCallback);
    }
}
