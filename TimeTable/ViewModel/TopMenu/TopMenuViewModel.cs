using System.Windows.Input;
using Common.Data.Notifier;
using Common.Messenger;
using Common.Messenger.Impl;
using Domain.Data.Enum;
using Domain.DomainContext;
using Domain.Event;
using TimeTable.ViewModel.TopMenu.Command;

namespace TimeTable.ViewModel.TopMenu
{
    public class TopMenuViewModel : Notifier, ITopMenuViewModel
    {
        public TopMenuViewModel(IDomainContext domainContext)
        {
            DomainContext = domainContext;
            InitializeCommand();
        }

        private IDomainContext DomainContext { get; }
        private IMessenger Messenger => DomainContext?.Messenger;

        public ICommand HighSchoolCommand { get; private set; }
        public ICommand FacultyCommand { get; private set; }
        public ICommand ChairCommand { get; private set; }
        public ICommand SpecialtyCommand { get; private set; }
        public ICommand SpecializationCommand { get; private set; }
        public ICommand EmployeeCommand { get; private set; }

        private void InitializeCommand()
        {
            HighSchoolCommand = new HighSchoolComman(this);
            FacultyCommand = new FacultyCommand(this);
            ChairCommand = new ChairCommand(this);
            SpecialtyCommand = new SpecialtyCommand(this);
            SpecializationCommand = new SpecializationCommand(this);
            EmployeeCommand = new EmployeeCommand(this);
        }

        public void SelectHighSchoolMenu()
        {
            Messenger?.Send(CommandName.SetEntryControl, new MenuChangedEventArgs(MenuItemName.HighSchool));
        }

        public void SelectFacultyMenu()
        {
            Messenger?.Send(CommandName.SetEntryControl, new MenuChangedEventArgs(MenuItemName.Faculty));
        }
        public void SelectChairMenu()
        {
            Messenger?.Send(CommandName.SetEntryControl, new MenuChangedEventArgs(MenuItemName.Chair));
        }
        public void SelectSpecialtyMenu()
        {
            Messenger?.Send(CommandName.SetEntryControl, new MenuChangedEventArgs(MenuItemName.Specialty));
        }
        public void SelectSpecializationMenu()
        {
            Messenger?.Send(CommandName.SetEntryControl, new MenuChangedEventArgs(MenuItemName.Specialization));
        }
        public void SelectEmployeeMenu()
        {
            Messenger?.Send(CommandName.SetEntryControl, new MenuChangedEventArgs(MenuItemName.Employee));
        }

    }

}
