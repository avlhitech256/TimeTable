using Domain.Data.SearchCriteria;

namespace Specialization.SearchCriteria
{
    public interface ISpecializationSearchCriteria : ISearchCriteria
    {
        long SpecialtyId { get; set; }
        string SpecialtyName { get; set; }
    }
}