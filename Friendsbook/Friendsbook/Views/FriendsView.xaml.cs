using Friendsbook.ViewModels;

namespace Friendsbook.Views;

public partial class FriendsView : ContentView
{
    public FriendsView()
    {
        InitializeComponent();
        BindingContext = new FriendsViewModel();
    }
}