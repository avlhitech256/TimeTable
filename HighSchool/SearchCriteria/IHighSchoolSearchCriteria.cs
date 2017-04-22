using Domain.Data.SearchCriteria;

namespace HighSchool.SearchCriteria
{
    public interface IHighSchoolSearchCriteria : ISearchCriteria
    {
        long RectorId { get; set; }
        string RectorName { get; set; }
    }
}
