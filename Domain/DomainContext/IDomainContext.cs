using Domain.Annotations;
using Domain.Messenger;
using Domain.Entry;

namespace Domain.DomainContext
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
