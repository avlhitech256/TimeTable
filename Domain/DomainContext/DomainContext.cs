using Common.Data.Notifier;
using Common.Messenger;
using DataService.Model;


namespace Domain.DomainContext
{
    public class DomainContext : Notifier, IDomainContext
    {
        #region Constructors

        public DomainContext()
        {
            Messenger = new Messenger();
            DBContext = new TimeTableEntities();
        }

        #endregion

        #region Properties

        public IMessenger Messenger { get; }
        public TimeTableEntities DBContext { get; }

        #endregion

    }

}
