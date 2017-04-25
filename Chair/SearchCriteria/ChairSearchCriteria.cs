using DataService.Constant;

namespace Chair.SearchCriteria
{
    public class ChairSearchCriteria : Domain.Data.SearchCriteria.SearchCriteria, IChairSearchCriteria
    {
        #region Members

        private long facultyId;
        private string facultyName;

        #endregion

        #region Constructors

        public ChairSearchCriteria()
        {
            FacultyId = 0;
            FacultyName = string.Empty;
        }

        #endregion

        #region Properties

        public long FacultyId
        {
            get
            {
                return facultyId;
            }

            set
            {
                if (facultyId != value)
                {
                    facultyId = value;
                    OnPropertyChanged();
                    OnSearchCriteriaChanged();
                }
            }
        }

        public string FacultyName
        {
            get
            {
                return facultyName;
            }

            set
            {
                if (facultyName != value)
                {
                    facultyName = value;
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
            FacultyId = 0;
            FacultyName = string.Empty;
        }

        protected override bool VerifyIsEmpty()
        {
            bool result = base.VerifyIsEmpty() &&
                          FacultyId <= 0 &&
                          (string.IsNullOrWhiteSpace(FacultyName) ||
                           FacultyName == DafaultConstant.DefaultFaculty);
            return result;
        }

        #endregion
    }

}
