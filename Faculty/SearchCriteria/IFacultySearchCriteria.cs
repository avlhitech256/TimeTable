using Domain.Data.SearchCriteria;

namespace Faculty.SearchCriteria
{
    public interface IFacultySearchCriteria : ISearchCriteria
    {
        long HighSchoolId { get; set; }
        string HighSchoolName { get; set; }
    }
}
