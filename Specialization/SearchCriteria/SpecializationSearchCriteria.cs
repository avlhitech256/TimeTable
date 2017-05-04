using DataService.Constant;

namespace Specialization.SearchCriteria
{
    internal class SpecializationSearchCriteria : Domain.Data.SearchCriteria.SearchCriteria,
        ISpecializationSearchCriteria
    {
        #region Members

        private long specialtyId;
        private string specialtyName;

        #endregion

        #region Constructors

        public SpecializationSearchCriteria()
        {
            SpecialtyId = 0;
            SpecialtyName = string.Empty;
        }

        #endregion

        #region Properties

        public long SpecialtyId
        {
            get { return specialtyId; }

            set
            {
                if (specialtyId != value)
                {
                    specialtyId = value;
                    OnPropertyChanged();
                    OnSearchCriteriaChanged();
                }
            }
        }

        public string SpecialtyName
        {
            get { return specialtyName; }

            set
            {
                if (specialtyName != value)
                {
                    specialtyName = value;
                    OnPropertyChanged();
                    OnSearchCriteriaChanged();
                }
            }
        }

        #endregion

        #region Methods

        public override void Clear()
        {
            base.Clear();
            SpecialtyId = 0;
            SpecialtyName = string.Empty;
        }

        protected override bool VerifyIsEmpty()
        {
            bool result = base.VerifyIsEmpty() &&
                          SpecialtyId <= 0 &&
                          (string.IsNullOrWhiteSpace(SpecialtyName) ||
                           SpecialtyName == DafaultConstant.DefaultFaculty);
            return result;
        }

        #endregion
    }
}