using System.Collections.Generic;
using System.Linq;
using DataService.Model;

namespace DataService.DataService
{
    public class DataService : IDataService
    {
        #region Members

        private TimeTableEntities dbContext;

        #endregion

        #region Constructors

        public DataService()
        {
            dbContext = new TimeTableEntities(); 
        }

        #endregion

        #region Properties

        public TimeTableEntities DBContext => dbContext;

        #endregion

        #region Methods

        public List<HighSchool> GetHighSchools()
        {
            return dbContext.HighSchools.ToList();
        }

        #endregion

    }

}
