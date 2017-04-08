using System;
using System.ComponentModel;
using DataService.Model;

namespace Domain.Entity.HighSchool
{
    public interface IHighSchoolEntity : INotifyPropertyChanged
    {
        long Position { get; set; }
        long Id { get; set; }
        string Code { get; set; }
        string Name { get; set; }
        bool Active { get; set; }
        DateTime Created { get; }
        DateTime LastModify { get; }
        string UserModify { get; }
        long Rector { get; set; }
        Employee Employee { get; set; }
        DataService.Model.HighSchool HighSchool { get; }
    }
}
