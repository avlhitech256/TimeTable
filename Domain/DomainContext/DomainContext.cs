using Common.Data.Notifier;
using Domain.Messenger;
using DataService.DataService;


namespace Domain.DomainContext
{
    public class DomainContext : Notifier, IDomainContext
    {
        #region Constructors

        public DomainContext()
        {
            Messenger = new Common.Messenger.Impl.Messenger();
            DataService = new DataService.DataService.DataService();
        }

        #endregion

        #region Properties

        public IMessenger Messenger { get; }
        public IDataService DataService { get; }

        #endregion

    }

}
