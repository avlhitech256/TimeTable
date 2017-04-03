using Common.Annotations;
using Common.Messenger;
using DataService.Model;

//using DataService.Model;

namespace Domain.DomainContext
{
    public interface IDomainContext
    {
        [CanBeNull]
        IMessenger Messenger { get; }

        [CanBeNull]
        TimeTableEntities DBContext { get; }

    }

}
