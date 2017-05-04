using System.Collections.ObjectModel;

namespace DataService.Entity.Specialization
{
    public interface ISpecializationEntity : IDomainEntity<Model.Specialization>
    {
        long SpecialtyId { get; set; }
        Model.Specialty Specialty { get; set; }
        ObservableCollection<Model.Chair> Chairs { get; }
    }

}
