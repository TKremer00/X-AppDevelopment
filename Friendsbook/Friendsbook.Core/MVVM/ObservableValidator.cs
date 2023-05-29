namespace Friendsbook.Core.MVVM
{
    public class ObservableValidator : CommunityToolkit.Mvvm.ComponentModel.ObservableValidator
    {
        public bool IsValid => !HasErrors;
    }
}
