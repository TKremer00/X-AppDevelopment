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
}
