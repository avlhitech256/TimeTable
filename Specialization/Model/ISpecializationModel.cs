using System.Collections.ObjectModel;
using DataService.Model;
using Domain.Model;

namespace Specialization.Model
{
    public interface ISpecializationModel : IModel<DataService.Model.Specialization>
    {
        ObservableCollection<Specialty> Specialties { get; }
        ObservableCollection<Specialty> SpecialtiesForSearch { get; }
        ObservableCollection<DataService.Model.Chair> Chairs { get; }
        void RefreshSpecialties();
        void RefreshChairs();

    }
}