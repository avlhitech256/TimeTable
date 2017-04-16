using System.Collections.ObjectModel;
using System.ComponentModel;
using Common.Event;
using DataService.Entity.HighSchool;
using DataService.Model;
using Domain.SearchCriteria;

namespace HighSchool.Model
{
    public interface IHighSchoolModel : INotifyPropertyChanged
    {
        ISearchCriteria SearchCriteria { get; }
        IHighSchoolEntity SelectedItem { get; set; }
        ObservableCollection<IHighSchoolEntity> Entities { get; }
        ObservableCollection<Employee> Employees { get; }
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
