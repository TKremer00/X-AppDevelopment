using FinalProject.Core.Extensions;

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
        var onBackgroundColor = Resources.FindValueRecursive<Color>("OnBackgroundColor");
        Resources.Add("OnBackgroundColorTransparent", Color.FromRgba(onBackgroundColor.Red, onBackgroundColor.Green, onBackgroundColor.Blue, 0.5));
    }
}
