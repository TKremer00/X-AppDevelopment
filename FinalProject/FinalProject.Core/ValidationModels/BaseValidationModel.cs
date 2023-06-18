using CommunityToolkit.Mvvm.ComponentModel;

namespace FinalProject.Core.ValidationModels
{
    public abstract class BaseValidationModel<T> : ObservableValidator
    {
        abstract internal T ConvertToModel();
    }
}
