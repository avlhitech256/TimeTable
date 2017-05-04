using Domain.ViewModel;
using System.Collections.ObjectModel;
using Chair.Model;
using Specialization.Model;
using DataService.Model;
using Domain.DomainContext;


namespace Specialization.ViewModel
{
    public class SpecializationViewModel : ViewModel<DataService.Model.Specialization>, ISpecializationViewModel
    {
        #region Constructors

        public SpecializationViewModel(IDomainContext context) : base(context, new SpecializationModel(context))
        {
            EditModeHeader = "Редактирование записи специализации";
            ReadOnlyHeader = "Просмотр записи специализации";
            EditLabel = ReadOnlyHeader;
        }

        #endregion
        
        #region Properties

        public ObservableCollection<Specialty> Specialties => (Model as ISpecializationModel)?.Specialties;

        public ObservableCollection<Specialty> SpecialtiesForSearch => (Model as ISpecializationModel)?.SpecialtiesForSearch;

        public ObservableCollection<DataService.Model.Chair> Chairs => (Model as ISpecializationModel)?.Chairs;

        #endregion
    }
}