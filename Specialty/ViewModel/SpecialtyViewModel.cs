using Domain.DomainContext;
using Domain.ViewModel;
using Specialty.Model;

namespace Specialty.ViewModel
{
    public class SpecialtyViewModel : ViewModel<DataService.Model.Specialty>, ISpecialtyViewModel
    {
        #region Constructors

        public SpecialtyViewModel(IDomainContext context) : base(context, new SpecialtyModel(context)) { }

        #endregion
    }

}
