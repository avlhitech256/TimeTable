#region FileInfo
// TimeTable, Employee
// EmployeeModel.cs
// Ilya Likhoshva
// Created: 08.05.2017 10:22
#endregion

using System.Collections.Generic;
using System.Linq;
using Domain.DomainContext;
using Domain.Model;
using Employee.SearchCriteria;

namespace Employee.Model
{
    public class EmployeeModel : Model<DataService.Model.Employee>, IEmployeeModel
    {
        #region Constructors

        public EmployeeModel(IDomainContext domainContext) : base(domainContext, new EmployeeSearchCriteria()) { }

        #endregion

        #region Methods

        protected override List<DataService.Model.Employee> SelectEntities()
        {
            List<DataService.Model.Employee> result = base.SelectEntities();
            EmployeeSearchCriteria searchCriteria = SearchCriteria as EmployeeSearchCriteria;

            if (searchCriteria != null)
            {
                result = base.SelectEntities()
                    .Where(x => string.IsNullOrWhiteSpace(searchCriteria.Code) ||
                                x.Code.ToUpperInvariant()
                                    .Contains(searchCriteria.Code.ToUpperInvariant())).ToList()
                    .Where(x => string.IsNullOrWhiteSpace(searchCriteria.Name) ||
                                x.Name.ToUpperInvariant()
                                    .Contains(searchCriteria.Name.ToUpperInvariant())).ToList()
                    .Where(x => !searchCriteria.Active || x.Active).ToList()
                    .Where(x => (!searchCriteria.CreatedFrom.HasValue ||
                                 x.Created >= searchCriteria.CreatedFrom.Value) &&
                                (!searchCriteria.CreatedTo.HasValue ||
                                 x.Created < searchCriteria.CreatedTo.Value.AddDays(1))).ToList()
                    .Where(x => (!searchCriteria.LastModifyFrom.HasValue ||
                                 x.LastModify >= searchCriteria.LastModifyFrom.Value) &&
                                (!searchCriteria.LastModifyTo.HasValue ||
                                 x.LastModify < searchCriteria.LastModifyTo.Value.AddDays(1))).ToList()
                    .Where(x => string.IsNullOrWhiteSpace(searchCriteria.UserModify) ||
                                x.UserModify.ToUpperInvariant()
                                    .Contains(searchCriteria.UserModify.ToUpperInvariant())).ToList();
            }

            return result;
        }

        #endregion

    }
}