using System.Runtime.CompilerServices;

namespace Friendsbook.Core.MVVM
{
    public class ObservableObject : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            OnPropertyChanging(propertyName);
            OnPropertyChanged(propertyName);
        }
    }
}
