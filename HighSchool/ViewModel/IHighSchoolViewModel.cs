using System;
using System.Collections.ObjectModel;
using Domain.Messenger;
using Domain.DomainContext;
using Domain.Entity.HighSchool;
using Domain.Entry;

namespace HighSchool.ViewModel
{
    public interface IHighSchoolViewModel : IViewModel
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
        long RectorId { get; set; }
        DateTime Created { get; }
        DateTime LastModify { get; }
        string UserModify { get; }
        void ApplySearchCriteria();
    }
}
