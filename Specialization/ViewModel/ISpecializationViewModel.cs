using System.Collections.ObjectModel;
using DataService.Model;
using Domain.ViewModel;

namespace Specialization.ViewModel
{
    public interface ISpecializationViewModel : IDataViewModel<DataService.Model.Specialization>
    { 
        ObservableCollection<Specialty> Specialties { get; }
        ObservableCollection<Specialty> SpecialtiesForSearch { get; }
        ObservableCollection<DataService.Model.Chair> Chairs { get; }
    }
}