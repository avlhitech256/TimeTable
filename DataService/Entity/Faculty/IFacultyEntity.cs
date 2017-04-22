using System.Collections.Generic;
using DataService.Model;

namespace DataService.Entity.Faculty
{
    public interface IFacultyEntity : IDomainEntity<Model.Faculty>
    {
        long HighSchoolId { get; set; }
        Model.HighSchool HighSchool { get; set; }
        ICollection<Chair> Chairs { get; }
    }

}
