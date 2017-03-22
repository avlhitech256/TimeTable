using Common.Messenger;

namespace Common.DomainContext
{
    public interface IDomainContext
    {
        IMessenger Messenger { get; }
    }
}
