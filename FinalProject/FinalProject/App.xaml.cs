namespace FinalProject;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }

    public override void CloseWindow(Window window)
    {
        base.CloseWindow(window);

        /*        var bluetoothLowEnergyConnection = Handler.MauiContext.Services.GetService<BluetoothLowEnergyService>();

                if (bluetoothLowEnergyConnection.HasBluetoothConnection())
                    bluetoothLowEnergyConnection.BluetoothDisconnection();*/
    }
}
