using System.Collections.ObjectModel;
using DataService.Model;
using Domain.DomainContext;
using Faculty.Model;
using Domain.ViewModel;

namespace Faculty.ViewModel
{
    public class FacultyViewModel : ViewModel<DataService.Model.Faculty>, IFacultyViewModel
    {
        #region Constructors

        public FacultyViewModel(IDomainContext context) : base(context, new FacultyModel(context)) { }

        #endregion

        #region Properties

        public ObservableCollection<HighSchool> HighSchools => (Model as IFacultyModel)?.HighSchools;

        #endregion
    }
}
