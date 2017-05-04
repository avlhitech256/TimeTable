using System.Collections.ObjectModel;

namespace DataService.Entity.Chair
{
    public interface IChairEntity : IDomainEntity<Model.Chair>
    {
        long? FacultyId { get; set; }
        Model.Faculty Faculty { get; set; }
        ObservableCollection<Model.Specialization> Specializations { get; }
    }

}
