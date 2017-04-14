using System.Windows.Input;

namespace Domain.Entry
{
    public interface IViewModel
    {
        ICommand BackToSearchButtonCommand { get; }
        ICommand NewButtonCommand { get; }
        ICommand ViewButtonCommand { get; }
        ICommand EditButtonCommand { get; }
        ICommand SaveButtonCommand { get; }
        ICommand SaveAndNewButtonCommand { get; }
        ICommand DeleteButtonCommand { get; }
        ICommand SearchButtonCommand { get; }
        ICommand ChangeEditModeButtonCommand { get; }
        string ToolTipForEditButton { get; }
        bool ReadOnly { get; set; }
        bool Enabled { get; set; }
        bool IsEditControl { get; set; }
    }
}
