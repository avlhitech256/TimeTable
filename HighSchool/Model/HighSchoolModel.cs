using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using System.Linq;
using DataService.Constant;
using DataService.Model;
using Domain.DomainContext;
using Domain.Model;
using HighSchool.SearchCriteria;

namespace HighSchool.Model
{
    public class HighSchoolModel : Model<DataService.Model.HighSchool>, IHighSchoolModel
    {
        #region Members

        private ObservableCollection<Employee> employees;
        private ObservableCollection<Employee> employeesForSearch;
        private bool employeesIsLoaded;
        private bool employeesForSearchIsLoaded;

        #endregion

        #region Constructors

        public HighSchoolModel(IDomainContext domainContext) : base(domainContext, new HighSchoolSearchCriteria())
        {
            employeesIsLoaded = false;
            employeesForSearchIsLoaded = false;
        }

        #endregion

        #region Properties

        public ObservableCollection<Employee> Employees
        {
            get
            {
                if (!employeesIsLoaded)
                {
                    employeesIsLoaded = true;
                    CreateEmployees();
                }

                return employees;
            }

            private set
            {
                if (employees != value)
                {
                    employees = value;
                    OnPropertyChanged();
                }

            }

        }

        public ObservableCollection<Employee> EmployeesForSearch
        {
            get
            {
                if (!employeesForSearchIsLoaded)
                {
                    employeesForSearchIsLoaded = true;
                    CreateEmployeesForSearch();
                }

                return employeesForSearch;
            }

            private set
            {
                if (employeesForSearch != value)
                {
                    employeesForSearch = value;
                    OnPropertyChanged();
                }
            }

        }

        #endregion

        #region Methods

        public void RefreshEmployees()
        {
            RefreshEmployeesProperty();
            RefreshEmployeesForSearchProperty();
        }

        private void CreateEmployees()
        {
            Employees = new ObservableCollection<Employee>();
            RefreshEmployeesProperty();
        }
        private void RefreshEmployeesProperty()
        {
            Employees.Clear();

            try
            {
                DbContext.Employees.OrderBy(x => x.Name).ToList().ForEach(x => Employees.Add(x));
                OnPropertyChanged(nameof(Employees));
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

        private void CreateEmployeesForSearch()
        {
            EmployeesForSearch = new ObservableCollection<Employee>();
            RefreshEmployeesForSearchProperty();
        }

        private void RefreshEmployeesForSearchProperty()
        {
            EmployeesForSearch.Clear();

            try
            {
                Employee item0 = new Employee { Id = 0, Name = DafaultConstant.DefaultRector };
                EmployeesForSearch.Add(item0);
                Employees.ToList().ForEach(x => EmployeesForSearch.Add(x));
                OnPropertyChanged(nameof(EmployeesForSearch));
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
                                    .Contains(searchCriteria.UserModify.ToUpperInvariant())).ToList()
                    .Where(x => searchCriteria.RectorId <= 0L || x.Rector == searchCriteria.RectorId).ToList();
            }

            return result;
        }

        #endregion
    }

}
