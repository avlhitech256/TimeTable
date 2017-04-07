using Domain.Annotations;
using Domain.Messenger;
using DataService.DataService;

namespace Domain.DomainContext
{
    public interface IDomainContext
    {
        [CanBeNull]
        IMessenger Messenger { get; }

        [CanBeNull]
        IDataService DataService { get; }

    }

}
