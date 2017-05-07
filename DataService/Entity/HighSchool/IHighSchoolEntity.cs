using DataService.Model;

namespace DataService.Entity.HighSchool
{
    public interface IHighSchoolEntity : IDomainEntity<Model.HighSchool>
    {
        long Rector { get; set; }
        Employee Employee { get; set; }
    }
}
