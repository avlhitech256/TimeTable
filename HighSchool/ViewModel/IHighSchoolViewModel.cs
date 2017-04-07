using System;
using System.Collections.ObjectModel;
using Common.Messenger;
using DataService.Entity.HighSchool;
using Domain.DomainContext;

namespace HighSchool.ViewModel
{
    public interface IHighSchoolViewModel
    {
        IDomainContext DomainContext { get; }
        IMessenger Messenger { get; }
        HighSchoolSearchCriteria SearchCriteria { get; }
        IHighSchoolEntity SelectedHighSchool { get; }
        ObservableCollection<IHighSchoolEntity> HighSchools { get; }
        ObservableCollection<DataService.Model.Employee> Employees { get; }
        string Code { get; set; }
        string Name { get; set; }
        bool Active { get; set; }
        DateTimeOffset Cteated { get; set; }
        DateTimeOffset LastModify { get; set; }
        string UserModify { get; set; }
        void ApplySearchCriteria();
    }
}
