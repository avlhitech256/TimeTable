using System.Collections.ObjectModel;

namespace HighSchool.Model
{
    public interface IHighSchoolModel
    {
        ObservableCollection<DataService.Model.HighSchool> HighSchools { get; set; }
    }
}
