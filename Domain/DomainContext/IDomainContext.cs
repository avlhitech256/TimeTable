using Common.Annotations;
using Common.Messenger;
using DataService.DataService;

//using DataService.Model;

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
