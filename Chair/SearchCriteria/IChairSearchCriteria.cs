using Domain.Data.SearchCriteria;

namespace Chair.SearchCriteria
{
    public interface IChairSearchCriteria : ISearchCriteria
    {
        long FacultyId { get; set; }
        string FacultyName { get; set; }
    }
}
