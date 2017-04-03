using System.Collections.Generic;
using DataService.Model;

namespace DataService.DataService
{
    public interface IDataService
    {
        TimeTableEntities DBContext { get; }
        List<HighSchool> GetHighSchools();
    }
}
