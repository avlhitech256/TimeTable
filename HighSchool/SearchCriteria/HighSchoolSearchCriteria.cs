namespace HighSchool.SearchCriteria
{
    public class HighSchoolSearchCriteria : Domain.Data.SearchCriteria.SearchCriteria, IHighSchoolSearchCriteria
    {
        #region Members

        private long rectorId;
        private string rectorName;

        #endregion

        #region Constructors

        public HighSchoolSearchCriteria()
        {
            RectorId = 0;
            RectorName = string.Empty;
        }

        #endregion

        #region Properties

        public long RectorId
        {
            get
            {
                return rectorId;
            }

            set
            {
                if (rectorId != value)
                {
                    rectorId = value;
                    OnPropertyChanged();
                    OnSearchCriteriaChanged();
                }
            }
        }

        public string RectorName
        {
            get
            {
                return rectorName;
            }

            set
            {
                if (rectorName != value)
                {
                    rectorName = value;
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
            RectorId = 0;
            RectorName = string.Empty;
        }

        protected override bool VerifyIsEmpty()
        {
            bool result = base.VerifyIsEmpty() &&
                          RectorId <= 0 &&
                          string.IsNullOrWhiteSpace(RectorName);
            return result;
        }

        #endregion

    }
}
