using System.Windows.Input;

namespace Domain.Entry
{
    public interface IViewModel
    {
        ICommand BackToSearchButtonCommand { get; }
        ICommand NewButtonCommand { get; }
        ICommand NewInSearchButtonCommand { get; }
        ICommand EditButtonCommand { get; }
        ICommand SaveButtonCommand { get; }
        ICommand DeleteButtonCommand { get; }
        ICommand SearchButtonCommand { get; }
        ICommand ChangeEditModeButtonCommand { get; }
        bool ReadOnly { get; set; }
        bool IsEditControl { get; set; }
    }
}
