using System.Windows.Input;

namespace Domain.Entry
{
    public interface IViewModel
    {
        ICommand BackButtonCommand { get; }
        ICommand ForwardButtonCommand { get; }
        ICommand NewButtonCommand { get; }
        ICommand EditButtonCommand { get; }
        ICommand SaveButtonCommand { get; }
        ICommand SearchButtonCommand { get; }
        bool ReadOnly { get; set; }
        bool IsEditControl { get; set; }
    }
}
