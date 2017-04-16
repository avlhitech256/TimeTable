using Common.Annotations;
using Common.Messenger;
using Common.ViewModel;

namespace Common.DomainContext
{
    public interface IDomainContext
    {
        [CanBeNull]
        IMessenger Messenger { get; }

        [CanBeNull]
        IViewModel ViewModel { get; set; }

        [CanBeNull]
        string UserName { get; }

        [CanBeNull]
        string UserDomain { get; }

        [CanBeNull]
        string Workstation { get; }

        [CanBeNull]
        string DataBaseServer { get; set; }
    }

}
