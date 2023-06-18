using FinalProject.Core.ViewModels;
using Material.Components.Maui;

namespace FinalProject.Pages;

public partial class AddPlantPage : ContentPage
{
    private readonly AddPlantPageViewModel _viewModel;

    public AddPlantPage(AddPlantPageViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    private void LoadedTextField(object sender, EventArgs e)
    {
        if (sender is not TextField textField)
        {
            return;
        }

        var binding = new Binding(textField.CommandParameter as string, mode: BindingMode.TwoWay, source: _viewModel.Plant);
        textField.SetBinding(TextField.TextProperty, binding);
    }

    private void LoadedCombobox(object sender, EventArgs e)
    {
        if (sender is not ComboBox comboboxField)
        {
            return;
        }

        var binding = new Binding(comboboxField.CommandParameter as string, mode: BindingMode.TwoWay, source: _viewModel.Plant);
        comboboxField.SetBinding(ComboBox.SelectedIndexProperty, binding);
    }
}