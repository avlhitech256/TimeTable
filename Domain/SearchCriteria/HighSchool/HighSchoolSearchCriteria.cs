namespace Domain.SearchCriteria.HighSchool
{
    public class HighSchoolSearchCriteria : SearchCriteria
    {
        #region Members

        private long rectorId;
        private string rectorName;

        #endregion

        #region Constructors

        public HighSchoolSearchCriteria() : base()
        {
            RectorId = -1;
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

    }
}
