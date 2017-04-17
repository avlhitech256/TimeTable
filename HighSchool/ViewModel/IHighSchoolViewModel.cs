using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Common.Messenger;
using Common.DomainContext;
using Common.ViewModel;
using DataService.Entity;
using DataService.Entity.HighSchool;
using DataService.Model;
using Domain.SearchCriteria;
using Domain.SearchCriteria.HighSchool;

namespace HighSchool.ViewModel
{
    public interface IHighSchoolViewModel : IViewModel, INotifyPropertyChanged
    {
        IDomainContext DomainContext { get; }
        IMessenger Messenger { get; }
        ISearchCriteria SearchCriteria { get; }
        IDomainEntity<DataService.Model.HighSchool> SelectedItem { get; set; }
        ObservableCollection<IDomainEntity<DataService.Model.HighSchool>> HighSchools { get; }
        ObservableCollection<Employee> Employees { get; }
        bool HasChanges { get; }
        string Code { get; set; }
        string Name { get; set; }
        bool Active { get; set; }
        long Rector { get; set; }
        DateTime Created { get; }
        DateTime LastModify { get; }
        string UserModify { get; }
        void ApplySearchCriteria();
        void Add();
        void View();
        void Edit();
        void Save();
        void SaveAndAdd();
        void Delete();
        void Back();
    }
}
