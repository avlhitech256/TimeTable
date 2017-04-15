using System;

namespace Domain.Data.Criteria
{
    public class SearchCriteria : Notifier.Notifier
    {
        #region Members

        private string code;
        private string name;
        private bool active;
        private DateTime? cteatedFrom;
        private DateTime? cteatedTo;
        private DateTime? lastModifyFrom;
        private DateTime? lastModifyTo;
        private string userModify;

        #endregion

        #region Constructors

        public SearchCriteria()
        {
            Code = string.Empty;
            Name = string.Empty;
            Active = true;
            CteatedFrom = null;
            CteatedTo = null;
            LastModifyFrom = null;
            LastModifyTo = null;
            UserModify = string.Empty;
        }

        #endregion

        #region Properties

        public string Code
        {
            get
            {
                return code;
            }

            set
            {
                if (code != value)
                {
                    code = value.ToUpper();
                    OnPropertyChanged();
                    OnSearchCriteriaChanged();
                }
            }

        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged();
                    OnSearchCriteriaChanged();
                }

            }
        }

        public bool Active
        {
            get
            {
                return active;
            }

            set
            {
                if (active != value)
                {
                    active = value;
                    OnPropertyChanged();
                    OnSearchCriteriaChanged();
                }

            }

        }

        public DateTime? CteatedFrom
        {
            get
            {
                return cteatedFrom;
            }

            set
            {
                if (cteatedFrom != value)
                {
                    cteatedFrom = value;
                    OnPropertyChanged();
                    OnSearchCriteriaChanged();
                }
            }
        }

        public DateTime? CteatedTo
        {
            get
            {
                return cteatedTo;
            }

            set
            {
                if (cteatedTo != value)
                {
                    cteatedTo = value;
                    OnPropertyChanged();
                    OnSearchCriteriaChanged();
                }
            }
        }

        public DateTime? LastModifyFrom
        {
            get
            {
                return lastModifyFrom;
            }

            set
            {
                if (lastModifyFrom != value)
                {
                    lastModifyFrom = value;
                    OnPropertyChanged();
                    OnSearchCriteriaChanged();
                }
            }
        }

        public DateTime? LastModifyTo
        {
            get
            {
                return lastModifyTo;
            }

            set
            {
                if (lastModifyTo != value)
                {
                    lastModifyTo = value;
                    OnPropertyChanged();
                    OnSearchCriteriaChanged();
                }
            }
        }

        public string UserModify
        {
            get
            {
                return userModify;
            }

            set
            {
                if (userModify != value)
                {
                    userModify = value;
                    OnPropertyChanged();
                    OnSearchCriteriaChanged();
                }

            }
        }

        protected virtual void OnSearchCriteriaChanged()
        {
            SearchCriteriaChanged?.Invoke(this, new EventArgs());
        }


        #endregion

        #region Events

        public event EventHandler SearchCriteriaChanged = delegate { };

        #endregion

    }
}
