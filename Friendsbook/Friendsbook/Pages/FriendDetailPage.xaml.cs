using Friendsbook.ViewModels;

namespace Friendsbook.Pages;

public partial class FriendDetailPage : ContentPage
{
    public FriendDetailPage(int id)
    {
        InitializeComponent();
        BindingContext = new FriendDetailPageViewModel(id);
    }
}