using System.Collections.ObjectModel;
using DataService.Model;
using Domain.Model;

namespace Faculty.Model
{
    public interface IFacultyModel : IModel<DataService.Model.Faculty>
    {
        ObservableCollection<Chair> Chair { get; }
    }
}
