using System.Collections.Generic;

namespace DataService.Entity.Chair
{
    public interface IChairEntity : IDomainEntity<Model.Chair>
    {
        long FacultyId { get; set; }
        Model.Faculty Faculty { get; set; }
        ICollection<Model.Specialization> Specializations { get; }

    }
}
