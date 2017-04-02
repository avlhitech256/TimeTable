using Common.Annotations;
using Common.Messenger;

namespace Domain.DomainContext
{
    public interface IDomainContext
    {
        [CanBeNull]
        IMessenger Messenger { get; }

        [CanBeNull]
        ViewModelRouter.ViewModelRouter ViewModelRouter { get; }

    }

}
