using System.Collections.ObjectModel;
using Common.Annotations;
using DataService.Model;
using Domain.ViewModel;

namespace Faculty.ViewModel
{
    public interface IFacultyViewModel : IDataViewModel<DataService.Model.Faculty>
    {
        [CanBeNull]
        ObservableCollection<HighSchool> HighSchools { get; }
    }
}
