using Friendsbook.ViewModels;

namespace Friendsbook.Pages;

public partial class MainPage : ContentPage
{
    private readonly MainPageViewModel _viewModel;

    public MainPage(MainPageViewModel mainPageViewModel)
    {
        InitializeComponent();
        _viewModel = mainPageViewModel;
        BindingContext = _viewModel;
        Shell.Current.Navigated += ApplicationNavigated;
    }

    private async void ApplicationNavigated(object sender, ShellNavigatedEventArgs e)
    {
        if (!e.Current.Location.OriginalString.EndsWith(nameof(MainPage)))
        {
            return;
        }

        await _viewModel.UpdateFriends();
    }
}

