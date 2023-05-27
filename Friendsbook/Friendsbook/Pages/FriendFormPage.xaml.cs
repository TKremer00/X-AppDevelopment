using Friendsbook.Core.Controllers;
using Friendsbook.ViewModels;

namespace Friendsbook.Pages;

public partial class FriendFormPage : ContentPage
{
    public FriendFormPage(FriendsController friendsController)
    {
        InitializeComponent();
        BindingContext = new FriendFormPageViewModel(friendsController);
    }
}