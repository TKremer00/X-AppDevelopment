using FinalProject.Core.ViewModels;
using Material.Components.Maui;

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

    private void TemperatureComboboxLoaded(object sender, EventArgs e)
    {
        if (sender is not ComboBox comboBox)
        {
            return;
        }

        var binding = new Binding(nameof(_viewModel.TemperatureSetting.ChosenIndex), mode: BindingMode.TwoWay, source: _viewModel.TemperatureSetting);
        comboBox.SetBinding(ComboBox.SelectedIndexProperty, binding);
    }

    private void EnvoirmentSpeedComboboxLoaded(object sender, EventArgs e)
    {
        if (sender is not ComboBox comboBox)
        {
            return;
        }

        var binding = new Binding(nameof(_viewModel.UpdateEnviromentSpeedSetting.ChosenIndex), mode: BindingMode.TwoWay, source: _viewModel.UpdateEnviromentSpeedSetting);
        comboBox.SetBinding(ComboBox.SelectedIndexProperty, binding);
    }
}