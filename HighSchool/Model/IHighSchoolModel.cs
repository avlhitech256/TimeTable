using System.Collections.ObjectModel;
using DataService.Model;
using Domain.Model;

namespace HighSchool.Model
{
    public interface IHighSchoolModel : IModel<DataService.Model.HighSchool>
    {
        ObservableCollection<Employee> Employees { get; }
        ObservableCollection<Employee> EmployeesForSearch { get; }
        void UpdateEmployees();
    }
}
