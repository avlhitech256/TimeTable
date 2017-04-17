using System.Collections.ObjectModel;
using Common.Annotations;
using DataService.Entity;
using Domain.SearchCriteria;

namespace Domain.ViewModel
{
    public interface IDataViewModel<T> : IControlViewModel where T : class 
    {
        [CanBeNull]
        ISearchCriteria SearchCriteria { get; }

        [CanBeNull]
        IDomainEntity<T> SelectedItem { get; set; }

        [CanBeNull]
        ObservableCollection<IDomainEntity<T>> Entities { get; }
    }
}
