using Common.Data.Notifier;
using DataService.Model;

namespace DataService.DataService
{
    public class DataService : Notifier, IDataService
    {
        #region Members

        private string userName;

        #endregion

        #region Constructors

        public DataService()
        {
            DBContext = new TimeTableEntities();
            userName = string.Empty;
        }

        #endregion

        #region Properties

        public TimeTableEntities DBContext { get; }

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
