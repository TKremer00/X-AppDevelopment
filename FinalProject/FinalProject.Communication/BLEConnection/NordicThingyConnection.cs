using FinalProject.Communication.Status;
using Shiny.BluetoothLE;

namespace FinalProject.Communication.BLEConnection
{
    public class NordicThingyConnection : INordicThingyConnection
    {
        private const string THINGY_53_UUID = "00000000-0000-0000-0000-ca5d92b32ecd";
        private const string PAIRING_CODE = "123456";
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
            await scanner.Start(predicate: (scanResult) => scanResult.Peripheral.Uuid == THINGY_53_UUID);
            await Task.Delay(TimeSpan.FromSeconds(10));
            statusUpdateCallback.Invoke(ConnectionStatus.Connection);

            var devices = scanner.Peripherals;

            // if i found the device/devices
            _nordicThingy = devices.FirstOrDefault()?.Peripheral;

            if (_nordicThingy == null)
            {
                // TODO: throw exception, couldn't find it.
            }

            await _nordicThingy.ConnectAsync(timeout: TimeSpan.FromSeconds(10));
            var pairingCode = _nordicThingy.TryPairingRequest(PAIRING_CODE);




            statusUpdateCallback.Invoke(ConnectionStatus.Connected);
            scanner.Stop();
        }

        private async Task Paired()
        {

        }

        private void TestMethod(BleCharacteristicResult obj)
        {

        }

        public Task OnAdapterStateChanged(AccessState state)
        {
            throw new NotImplementedException();
        }

        public Task OnPeripheralStateChanged(IPeripheral peripheral)
        {
            throw new NotImplementedException();
        }

        public async Task WriteMessage(Guid guid)
        {
        }
    }
}
