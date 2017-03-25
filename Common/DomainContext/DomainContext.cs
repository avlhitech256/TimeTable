using Common.Messenger;
using Common.ViewModel;

namespace Common.DomainContext
{
    public class DomainContext : IDomainContext
    {
        public DomainContext()
        {
            Messenger = new Messenger.Messenger();
            MainViewModel = new MainViewModel();
        }

        public IMessenger Messenger { get; }
        public MainViewModel MainViewModel { get; }

    }

}
