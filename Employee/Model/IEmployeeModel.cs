#region FileInfo
// TimeTable, Employee
// IEmployeeModel.cs
// Ilya Likhoshva
// Created: 08.05.2017 10:19
#endregion
using Domain.Model;
namespace Employee.Model
{
    public interface IEmployeeModel : IModel<DataService.Model.Employee>
    {
    }
}