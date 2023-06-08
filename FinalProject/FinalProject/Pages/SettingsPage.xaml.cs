using FinalProject.Core.ViewModels;
using Material.Components.Maui.Core;

namespace FinalProject.Pages;

public partial class SettingsPage : ContentPage
{
    private readonly SettingsPageViewModel _viewModel;

    public SettingsPage(SettingsPageViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    private void TemperatureUnitSelectedIndexChanged(object sender, SelectedIndexChangedEventArgs e)
    {
        _viewModel.ChosenTemperatureUnitIndex = e.SelectedIndex;
    }

    private void EnvoirmentSpeedSelectedIndexChanged(object sender, SelectedIndexChangedEventArgs e)
    {
        _viewModel.ChosenUpdateEnviromentSpeedIndex = e.SelectedIndex;
    }
}