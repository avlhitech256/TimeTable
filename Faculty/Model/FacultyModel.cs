using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using System.Linq;
using DataService.Constant;
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
        private bool highSchoolsIsLoaded;

        #endregion

        #region Constructors

        public FacultyModel(IDomainContext domainContext) : base(domainContext, new FacultySearchCriteria())
        {
            highSchoolsIsLoaded = false;
        }

        #endregion

        #region Properties

        public ObservableCollection<HighSchool> HighSchools
        {
            get
            {
                if (!highSchoolsIsLoaded)
                {
                    try
                    {
                        highSchools = new ObservableCollection<HighSchool>();
                        HighSchool item0 = new HighSchool { Id = 0, Name = DafaultConstant.DefaultHighSchool };
                        highSchools.Add(item0);
                        DbContext.HighSchools.OrderBy(x => x.Name).ToList().ForEach(x => highSchools.Add(x));
                        OnPropertyChanged();
                        highSchoolsIsLoaded = true;
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

                return highSchools;
            }

        }

        #endregion

        #region Methods

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
                    .Where(x => searchCriteria.HighSchoolId <= 0L || x.HighSchoolId == searchCriteria.HighSchoolId).ToList();
            }

            return result;
        }

        #endregion
    }

}
