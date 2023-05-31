using Friendsbook.ViewModels;

namespace Friendsbook.Pages;

public partial class FriendFormPage : ContentPage
{
    public FriendFormPage(FriendFormPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}