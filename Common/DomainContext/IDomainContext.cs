using Common.Annotations;
using Common.Entry;
using Common.Messenger;
using Common.ViewModel;

namespace Common.DomainContext
{
    public interface IDomainContext
    {
        [CanBeNull]
        IMessenger Messenger { get; }

        [CanBeNull]
        MainViewModel MainViewModel { get; }
    }
}
