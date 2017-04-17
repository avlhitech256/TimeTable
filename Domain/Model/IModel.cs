using System.Collections.ObjectModel;
using DataService.Entity;

namespace Domain.Model
{
    public interface IModel<T> where T : class
    {
        SearchCriteria.ISearchCriteria SearchCriteria { get; }
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
