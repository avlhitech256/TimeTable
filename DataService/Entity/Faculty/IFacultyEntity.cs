using System.Collections.Generic;

namespace DataService.Entity.Faculty
{
    public interface IFacultyEntity : IDomainEntity<Model.Faculty>
    {
        long HighSchoolId { get; set; }
        Model.HighSchool HighSchool { get; set; }
        ICollection<Model.Chair> Chairs { get; }
    }

}
