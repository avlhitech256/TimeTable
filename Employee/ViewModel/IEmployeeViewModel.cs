#region FileInfo
// TimeTable, Employee
// IEmployeeViewModel.cs
// Ilya Likhoshva
// Created: 08.05.2017 10:29
#endregion

using Domain.ViewModel;

namespace Employee.ViewModel
{
    public interface IEmployeeViewModel : IDataViewModel<DataService.Model.Employee>
    {
    }
}