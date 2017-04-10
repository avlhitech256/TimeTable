using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Domain.Messenger;
using Domain.DomainContext;
using Domain.Entity.HighSchool;
using Domain.Entry;

namespace HighSchool.ViewModel
{
    public interface IHighSchoolViewModel : IViewModel, INotifyPropertyChanged
    {
        IDomainContext DomainContext { get; }
        IMessenger Messenger { get; }
        HighSchoolSearchCriteria SearchCriteria { get; }
        IHighSchoolEntity SelectedItem { get; }
        ObservableCollection<IHighSchoolEntity> HighSchools { get; }
        ObservableCollection<DataService.Model.Employee> Employees { get; }
        bool HasChanges { get; }
        string Code { get; set; }
        string Name { get; set; }
        bool Active { get; set; }
        long RectorId { get; set; }
        DateTime Created { get; }
        DateTime LastModify { get; }
        string UserModify { get; }
        void ApplySearchCriteria();
        void Edit();
        void Save();
        void Delete();
        void Back();
    }
}
