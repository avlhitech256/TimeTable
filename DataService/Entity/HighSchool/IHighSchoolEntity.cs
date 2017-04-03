using System.ComponentModel;

namespace DataService.Entity.HighSchool
{
    public interface IHighSchoolEntity //: INotifyPropertyChanged
    {
        long Id { get; set; }
        string Name { get; set; }
    }
}
