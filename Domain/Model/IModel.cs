using System.Collections.ObjectModel;
using System.ComponentModel;
using DataService.Entity;
using Domain.Data.SearchCriteria;

namespace Domain.Model
{
    public interface IModel<T> : INotifyPropertyChanged where T : class
    {
        ISearchCriteria SearchCriteria { get; }
        IDomainEntity<T> SelectedItem { get; set; }
        ObservableCollection<IDomainEntity<T>> Entities { get; }
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
