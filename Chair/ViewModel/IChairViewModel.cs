using System.Collections.ObjectModel;
using DataService.Model;
using Domain.ViewModel;

namespace Chair.ViewModel
{
    public interface IChairViewModel : IDataViewModel<DataService.Model.Chair>
    {
        ObservableCollection<Faculty> Faculties { get; }
        ObservableCollection<Faculty> FacultiesForSearch { get; }
        ObservableCollection<Specialization> Specializations { get; }
    }

}
