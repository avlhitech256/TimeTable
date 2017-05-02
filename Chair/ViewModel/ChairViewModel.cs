using Domain.ViewModel;
using System.Collections.ObjectModel;
using Chair.Model;
using DataService.Model;
using Domain.DomainContext;

namespace Chair.ViewModel
{
    public class ChairViewModel : ViewModel<DataService.Model.Chair>, IChairViewModel
    {
        #region Constructors

        public ChairViewModel(IDomainContext context) : base(context, new ChairModel(context))
        {
            EditModeHeader = "Редактирование записи кафедры";
            ReadOnlyHeader = "Просмотр записи кафедры";
            EditLabel = ReadOnlyHeader;
        }

        #endregion

        #region Properties

        public ObservableCollection<Faculty> Faculties => (Model as IChairModel)?.Faculties;

        public ObservableCollection<Faculty> FacultiesForSearch => (Model as IChairModel)?.FacultiesForSearch;

        public ObservableCollection<Specialization> Specializations => (Model as IChairModel)?.Specializations;

        #endregion
    }

}
