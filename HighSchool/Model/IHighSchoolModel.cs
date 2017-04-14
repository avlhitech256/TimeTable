using System.Collections.ObjectModel;
using System.ComponentModel;
using Domain.Entity.HighSchool;
using Domain.Event;
using HighSchool.ViewModel;

namespace HighSchool.Model
{
    public interface IHighSchoolModel : INotifyPropertyChanged
    {
        HighSchoolSearchCriteria SearchCriteria { get; }
        IHighSchoolEntity SelectedHighSchool { get; set; }
        ObservableCollection<IHighSchoolEntity> HighSchools { get; }
        ObservableCollection<DataService.Model.Employee> Employees { get; }
        bool HasChanges { get; }
        string DataBaseServer { get; }
        void ApplySearchCriteria();
        void Add();
        void Save();
        void Delete();
        void Rollback();
        bool ValidateRequiredCode();
        bool ValidateUniqueCode();

        event EntityExceptionEventHandler EntityException;
    }
}
