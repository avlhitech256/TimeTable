using DataService.Model;

namespace DataService.Entity.HighSchool
{
    public interface IHighSchoolEntity : IDomainEntity<Model.HighSchool>
    {
        long Rector { get; set; }
        Model.Employee Employee { get; set; }
    }
}
