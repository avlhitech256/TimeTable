using Common.Messenger;

namespace Common.DomainContext
{
    public class DomainContext : IDomainContext
    {
        #region Members

        private static DomainContext context;

        #endregion
        private DomainContext()
        {
            Messenger = new Messenger.Messenger();
        }

        public static DomainContext Instance()
        {
            return context ?? (context = new DomainContext());
        }

        public IMessenger Messenger { get; }
    }

}
