using System.Collections.ObjectModel;

namespace DataService.Entity.Faculty
{
    public interface IFacultyEntity : IDomainEntity<Model.Faculty>
    {
        long? HighSchoolId { get; set; }
        Model.HighSchool HighSchool { get; set; }
        ObservableCollection<Model.Chair> Chairs { get; }
    }

}
