namespace FinalProject;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
        AddCustomResources();
    }

    private void AddCustomResources()
    {
        var onBackgroundColor = FindValueInResources<Color>("OnBackgroundColor");
        Resources.Add("OnBackgroundColorTransparent", Color.FromRgba(onBackgroundColor.Red, onBackgroundColor.Green, onBackgroundColor.Blue, 0.5));
    }

    private T FindValueInResources<T>(string key)
    {
        foreach (var resourceDictionary in Resources.MergedDictionaries.Prepend(Resources))
        {
            if (resourceDictionary.TryGetValue(key, out var value))
            {
                return (T)value;
            }
        }

        throw new Exception("could not find resource");
    }

    public override void CloseWindow(Window window)
    {
        base.CloseWindow(window);

        /*        var bluetoothLowEnergyConnection = Handler.MauiContext.Services.GetService<BluetoothLowEnergyService>();

                if (bluetoothLowEnergyConnection.HasBluetoothConnection())
                    bluetoothLowEnergyConnection.BluetoothDisconnection();*/
    }
}
