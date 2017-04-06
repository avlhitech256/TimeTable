using System;
using System.ComponentModel;
using DataService.Model;

namespace DataService.Entity.HighSchool
{
    public interface IHighSchoolEntity //: INotifyPropertyChanged
    {
        long Position { get; set; }
        long Id { get; set; }
        string Code { get; set; }
        string Name { get; set; }
        bool Active { get; set; }
        DateTimeOffset Cteated { get; set; }
        DateTimeOffset LastModify { get; set; }
        string UserModify { get; set; }
        long Rector { get; set; }
        Employee Employee { get; set; }
        Model.HighSchool HighSchool { get; set; }
    }
}
