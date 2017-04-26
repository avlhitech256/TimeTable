using System.Collections.ObjectModel;
using Common.Annotations;
using DataService.Model;
using Domain.ViewModel;

namespace HighSchool.ViewModel
{
    public interface IHighSchoolViewModel : IDataViewModel<DataService.Model.HighSchool>
    {
        [CanBeNull]
        ObservableCollection<Employee> Employees { get; }

        [CanBeNull]
        ObservableCollection<Employee> EmployeesForSearch { get; }
    }
}
