using Common.Annotations;
using Common.Messenger;
using Domain.ViewModel;

namespace Domain.DomainContext
{
    public interface IDomainContext
    {
        [CanBeNull]
        IMessenger Messenger { get; }

        [CanBeNull]
        IControlViewModel ViewModel { get; set; }

        bool IsEditControl { get; set; }

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
