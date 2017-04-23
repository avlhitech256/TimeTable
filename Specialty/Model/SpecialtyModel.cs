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
    }
}
