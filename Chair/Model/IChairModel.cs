using System.Collections.ObjectModel;
using DataService.Model;
using Domain.Model;

namespace Chair.Model
{
    public interface IChairModel : IModel<DataService.Model.Chair>
    {
        ObservableCollection<Faculty> Faculties { get; }
        ObservableCollection<Faculty> FacultiesForSearch { get; }
        ObservableCollection<Specialization> Specializations { get; }
        void RefreshFaculties();
        void RefreshSpecializations();

    }
}
