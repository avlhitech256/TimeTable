#region FileInfo
// TimeTable, Employee
// EmployeeViewModel.cs
// Ilya Likhoshva
// Created: 08.05.2017 10:30
#endregion

using Domain.DomainContext;
using Domain.ViewModel;
using Employee.Model;

namespace Employee.ViewModel
{
    public class EmployeeViewModel : ViewModel<DataService.Model.Employee>, IEmployeeViewModel
    {
        #region Constructors

        public EmployeeViewModel(IDomainContext context) : base(context, new EmployeeModel(context))
        {
            EditModeHeader = "Редактирование записи работника";
            ReadOnlyHeader = "Просмотр записи работника";
            EditLabel = ReadOnlyHeader;
        }

        #endregion
    }
}