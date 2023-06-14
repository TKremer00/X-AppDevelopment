using FinalProject.Communication.Status;
using Shiny.BluetoothLE;

namespace FinalProject.Communication.BLEConnection
{
    public class NordicThingyConnection : INordicThingyConnection
    {
        private readonly IBleManager _bleManager;
        private IPeripheral _nordicThingy;

        public NordicThingyConnection(IBleManager bleManager)
        {
            _bleManager = bleManager;
        }

        public async Task ConnectAsync(Action<ConnectionStatus> statusUpdateCallback)
        {
            var scanner = _bleManager.CreateManagedScanner();

            statusUpdateCallback.Invoke(ConnectionStatus.Scanning);
            await scanner.Start();
            await Task.Delay(TimeSpan.FromSeconds(10));
            statusUpdateCallback.Invoke(ConnectionStatus.Connection);

            var devices = scanner.Peripherals;

            // if i found the device/devices
            _nordicThingy = devices.First().Peripheral;

            await _nordicThingy.ConnectAsync(timeout: TimeSpan.FromSeconds(10));

            statusUpdateCallback.Invoke(ConnectionStatus.Connected);
            scanner.Stop();
        }

        public async Task WriteMessage(Guid guid)
        {
        }
    }
}
