using System.Collections.Generic;
using System.Linq;
using Common.Data.Notifier;
using DataService.Model;

namespace DataService.DataService
{
    public class DataService : Notifier, IDataService
    {
        #region Members

        private TimeTableEntities dbContext;
        private string userName;

        #endregion

        #region Constructors

        public DataService()
        {
            dbContext = new TimeTableEntities();
            userName = string.Empty;
        }

        #endregion

        #region Properties

        public TimeTableEntities DBContext => dbContext;

        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                if (userName != value)
                {
                    userName = value;
                    OnPropertyChanged();
                }
            }

        }

        #endregion

    }

}
