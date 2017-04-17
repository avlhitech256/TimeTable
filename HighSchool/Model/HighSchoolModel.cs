using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using System.Linq;
using Common.DomainContext;
using DataService.Model;
using Domain.Model;
using Domain.SearchCriteria.HighSchool;

namespace HighSchool.Model
{
    public class HighSchoolModel : Model<DataService.Model.HighSchool>, IHighSchoolModel, INotifyPropertyChanged
    {
        #region Members

        private ObservableCollection<Employee> employees;
        private bool employeesIsLoaded;

        #endregion

        #region Constructors

        public HighSchoolModel(IDomainContext domainContext) : base(domainContext)
        {
            employeesIsLoaded = false;
        }

        #endregion

        #region Properties

        public ObservableCollection<Employee> Employees
        {
            get
            {
                if (!employeesIsLoaded)
                {
                    try
                    {
                        employees = new ObservableCollection<Employee>();
                        Employee item0 = new Employee() { Id = 0, Name = "Любой ректор" };
                        employees.Add(item0);
                        DbContext.Employees.OrderBy(x => x.Name).ToList().ForEach(x => employees.Add(x));
                        OnPropertyChanged();
                        employeesIsLoaded = true;
                    }
                    catch (EntityException e)
                    {
                        OnEntityException(e);
                    }
                    catch (DbEntityValidationException e)
                    {
                        OnDbEntityValidationException(e);
                    }

                }

                return employees;
            }

        }

        #endregion

        #region Methods

        protected override List<DataService.Model.HighSchool> SelectEntities()
        {
            List<DataService.Model.HighSchool> result = base.SelectEntities();
            HighSchoolSearchCriteria searchCriteria = SearchCriteria as HighSchoolSearchCriteria;

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
                    .Where(x => (!searchCriteria.CteatedFrom.HasValue ||
                                 x.Created >= searchCriteria.CteatedFrom.Value) &&
                                (!searchCriteria.CteatedTo.HasValue ||
                                 x.Created < searchCriteria.CteatedTo.Value.AddDays(1))).ToList()
                    .Where(x => (!searchCriteria.LastModifyFrom.HasValue ||
                                 x.LastModify >= searchCriteria.LastModifyFrom.Value) &&
                                (!searchCriteria.LastModifyTo.HasValue ||
                                 x.LastModify < searchCriteria.LastModifyTo.Value.AddDays(1))).ToList()
                    .Where(x => string.IsNullOrWhiteSpace(searchCriteria.UserModify) ||
                                x.UserModify.ToUpperInvariant()
                                    .Contains(searchCriteria.UserModify.ToUpperInvariant())).ToList()
                    .Where(x => searchCriteria.RectorId <= 0L || x.Rector == searchCriteria.RectorId).ToList();
            }

            return result;
        }

        #endregion
    }

}
