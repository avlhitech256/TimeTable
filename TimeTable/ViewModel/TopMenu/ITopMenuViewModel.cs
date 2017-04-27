using System.Windows.Input;

namespace TimeTable.ViewModel.TopMenu
{
    public interface ITopMenuViewModel
    {
        ICommand HighSchoolCommand { get; }
        ICommand FacultyCommand { get; }
        ICommand ChairCommand { get; }
        ICommand SpecialtyCommand { get; }
        ICommand SpecializationCommand { get; }
        ICommand EmployeeCommand { get; }
        void SelectHighSchoolMenu();
        void SelectFacultyMenu();
        void SelectChairMenu();
        void SelectSpecialtyMenu();
        void SelectSpecializationMenu();
        void SelectEmployeeMenu();
    }
}
