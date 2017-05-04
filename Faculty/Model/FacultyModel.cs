using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using System.Linq;
using DataService.Constant;
using DataService.Entity.Faculty;
using DataService.Model;
using Domain.DomainContext;
using Domain.Model;
using Faculty.SearchCriteria;

namespace Faculty.Model
{
    public class FacultyModel : Model<DataService.Model.Faculty>, IFacultyModel
    {
        #region Members

        private ObservableCollection<HighSchool> highSchools;
        private ObservableCollection<HighSchool> highSchoolsForSearch;

        #endregion

        #region Constructors

        public FacultyModel(IDomainContext domainContext) : base(domainContext, new FacultySearchCriteria()) { }

        #endregion

        #region Properties

        public ObservableCollection<HighSchool> HighSchools
        {
            get
            {
                if (highSchools == null)
                {
                    CreateHighSchools();
                }

                return highSchools;
            }

            private set
            {
                if (highSchools != value)
                {
                    highSchools = value;
                    OnPropertyChanged();
                }

            }

        }

        public ObservableCollection<HighSchool> HighSchoolsForSearch
        {
            get
            {
                if (highSchoolsForSearch == null)
                {
                    CreateHighSchoolsForSearch();
                }

                return highSchoolsForSearch;
            }

            private set
            {
                if (highSchoolsForSearch != value)
                {
                    highSchoolsForSearch = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Chair> Chairs => ((IFacultyEntity) SelectedItem).Chairs;

        #endregion

        #region Methods

        public void RefreshHighSchools()
        {
            RefreshHighSchoolsProperty();
            RefreshHighSchoolsForSearchProperty();
        }

        private void CreateHighSchools()
        {
            HighSchools = new ObservableCollection<HighSchool>();
            RefreshHighSchoolsProperty();
        }

        public void RefreshHighSchoolsProperty()
        {
            HighSchools.Clear();

            try
            {
                HighSchool item0 = new HighSchool { Id = 0, Name = string.Empty };
                HighSchools.Add(item0);
                DbContext.HighSchools.OrderBy(x => x.Name).ToList().ForEach(x => HighSchools.Add(x));
                OnPropertyChanged(nameof(HighSchools));
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

        private void CreateHighSchoolsForSearch()
        {
            HighSchoolsForSearch = new ObservableCollection<HighSchool>();
            RefreshHighSchoolsForSearchProperty();
        }

        public void RefreshHighSchoolsForSearchProperty()
        {
            HighSchools.Clear();

            try
            {
                HighSchool item0 = new HighSchool { Id = 0, Name = DafaultConstant.DefaultHighSchool };
                HighSchoolsForSearch.Add(item0);
                DbContext.HighSchools.OrderBy(x => x.Name).ToList().ForEach(x => HighSchoolsForSearch.Add(x));
                OnPropertyChanged(nameof(HighSchoolsForSearch));
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

        public void RefreshChairs()
        {
            SelectedItem.RefreshChildItems();
        }

        protected override List<DataService.Model.Faculty> SelectEntities()
        {
            List<DataService.Model.Faculty> result = base.SelectEntities();
            FacultySearchCriteria searchCriteria = SearchCriteria as FacultySearchCriteria;

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
                    .Where(x => searchCriteria.HighSchoolId <= 0L || x.HighSchoolId == searchCriteria.HighSchoolId).ToList();
            }

            return result;
        }

        #endregion
    }

}
