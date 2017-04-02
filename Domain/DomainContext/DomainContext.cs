using Common.Data.Notifier;
using Common.Event;
using Common.Messenger;
using Common.Messenger.Impl;

namespace Domain.DomainContext
{
    public class DomainContext : Notifier, IDomainContext
    {
        #region Constructors

        public DomainContext()
        {
            Messenger = new Messenger();
            ViewModelRouter = new ViewModelRouter.ViewModelRouter();
        }

        #endregion

        #region Properties

        public IMessenger Messenger { get; }
        public ViewModelRouter.ViewModelRouter ViewModelRouter { get; }

        #endregion

    }

}
