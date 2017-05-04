using System.Collections.Generic;
using System.Linq;
using Domain.DomainContext;
using Domain.Model;
using Specialty.SearchCriteria;

namespace Specialty.Model
{
    public class SpecialtyModel : Model<DataService.Model.Specialty>, ISpecialtyModel
    {
        #region Constructors

        public SpecialtyModel(IDomainContext domainContext) : base(domainContext, new SpecialtySearchCriteria()) { }

        #endregion

        #region Methods

        protected override List<DataService.Model.Specialty> SelectEntities()
        {
            List<DataService.Model.Specialty> result = base.SelectEntities();
           SpecialtySearchCriteria searchCriteria = SearchCriteria as SpecialtySearchCriteria;

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
