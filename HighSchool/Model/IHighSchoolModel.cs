using System.Collections.ObjectModel;
using System.ComponentModel;
using DataService.Entity.HighSchool;
using HighSchool.ViewModel;

namespace HighSchool.Model
{
    public interface IHighSchoolModel : INotifyPropertyChanged
    {
        HighSchoolSearchCriteria SearchCriteria { get; }
        IHighSchoolEntity SelectedHighSchool { get; }
        ObservableCollection<IHighSchoolEntity> HighSchools { get; }
        ObservableCollection<DataService.Model.Employee> Employees { get; }

        void ApplySearchCriteria();
    }
}
