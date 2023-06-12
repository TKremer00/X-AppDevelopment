using FinalProject.Core.ViewModels;
using Material.Components.Maui;

namespace FinalProject.Pages;

public partial class PlantsPage : ContentPage
{
    private readonly PlantsPageViewModel _viewModel;

    public PlantsPage(PlantsPageViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    private void PlantSearchFieldLoaded(object sender, EventArgs e)
    {
        if (sender is not TextField textField)
        {
            return;
        }

        var binding = new Binding(nameof(_viewModel.SearchPlantName), mode: BindingMode.TwoWay, source: _viewModel);
        textField.SetBinding(TextField.TextProperty, binding);
    }
}