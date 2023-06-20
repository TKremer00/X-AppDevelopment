using FinalProject.Communication.Communication;
using FinalProject.Core.Helpers;

namespace FinalProject;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
    }

    protected override bool OnBackButtonPressed()
    {
        RoutingHelper.NavigateBackUpdate();
        return base.OnBackButtonPressed();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        var bluetoothNotifier = Handler.MauiContext.Services.GetService<IBluetoothNotifier>();
        bluetoothNotifier.Dispose();
    }
}
