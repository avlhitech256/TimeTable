using DataService.Constant;

namespace Faculty.SearchCriteria
{
    public class FacultySearchCriteria : Domain.Data.SearchCriteria.SearchCriteria, IFacultySearchCriteria
    {
        #region Members

        private long highSchoolId;
        private string highSchoolName;

        #endregion

        #region Constructors

        public FacultySearchCriteria()
        {
            HighSchoolId = 0;
            HighSchoolName = string.Empty;
        }

        #endregion

        #region Properties

        public long HighSchoolId
        {
            get
            {
                return highSchoolId;
            }

            set
            {
                if (highSchoolId != value)
                {
                    highSchoolId = value;
                    OnPropertyChanged();
                    OnSearchCriteriaChanged();
                }
            }
        }

        public string HighSchoolName
        {
            get
            {
                return highSchoolName;
            }

            set
            {
                if (highSchoolName != value)
                {
                    highSchoolName = value;
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
            HighSchoolId = 0;
            HighSchoolName = string.Empty;
        }

        protected override bool VerifyIsEmpty()
        {
            bool result = base.VerifyIsEmpty() &&
                          HighSchoolId <= 0 &&
                          (string.IsNullOrWhiteSpace(HighSchoolName) ||
                           HighSchoolName == DafaultConstant.DefaultHighSchool);
            return result;
        }

        #endregion
    }

}
