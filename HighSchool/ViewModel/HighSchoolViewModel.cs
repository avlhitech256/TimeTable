using System.Collections.ObjectModel;
using Common.Annotations;
using DataService.Model;
using Domain.DomainContext;
using Domain.ViewModel;
using HighSchool.Model;

namespace HighSchool.ViewModel
{
    public class HighSchoolViewModel : ViewModel<DataService.Model.HighSchool>, IHighSchoolViewModel
    {
        #region Constructors

        public HighSchoolViewModel(IDomainContext context) : base(context, new HighSchoolModel(context)) { }

        #endregion

        #region Properties

        [CanBeNull]
        public ObservableCollection<Employee> Employees => (Model as IHighSchoolModel)?.Employees;

        [CanBeNull]
        public ObservableCollection<Employee> EmployeesForSearch => (Model as IHighSchoolModel)?.EmployeesForSearch;

        #endregion
    }
}
