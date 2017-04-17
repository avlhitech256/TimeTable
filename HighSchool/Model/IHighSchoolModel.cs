using System.Collections.ObjectModel;
using System.ComponentModel;
using DataService.Entity;
using DataService.Model;
using Domain.SearchCriteria;

namespace HighSchool.Model
{
    public interface IHighSchoolModel : INotifyPropertyChanged
    {
        ISearchCriteria SearchCriteria { get; }
        IDomainEntity<DataService.Model.HighSchool> SelectedItem { get; set; }
        ObservableCollection<IDomainEntity<DataService.Model.HighSchool>> Entities { get; }
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
    }
}
