using System;
using Common.Data.Notifier;

namespace Domain.Data.SearchCriteria
{
    public class SearchCriteria : Notifier, ISearchCriteria
    {
        #region Members

        private string code;
        private string name;
        private bool active;
        private DateTime? createdFrom;
        private DateTime? createdTo;
        private DateTime? lastModifyFrom;
        private DateTime? lastModifyTo;
        private string userModify;
        private bool isEmpty;

        #endregion

        #region Constructors

        public SearchCriteria()
        {
            Code = string.Empty;
            Name = string.Empty;
            Active = true;
            CreatedFrom = null;
            CreatedTo = null;
            LastModifyFrom = null;
            LastModifyTo = null;
            UserModify = string.Empty;
            IsEmpty = true;
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
                    code = value.ToUpper().Trim();
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

        public DateTime? CreatedFrom
        {
            get
            {
                return createdFrom;
            }

            set
            {
                if (createdFrom != value)
                {
                    createdFrom = value;
                    OnPropertyChanged();
                    OnSearchCriteriaChanged();
                }
            }
        }

        public DateTime? CreatedTo
        {
            get
            {
                return createdTo;
            }

            set
            {
                if (createdTo != value)
                {
                    createdTo = value;
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

        public bool IsEmpty
        {
            get
            {
                return isEmpty;
            }

            protected set
            {
                if (isEmpty != value)
                {
                    isEmpty = value;
                    OnPropertyChanged();
                    OnSearchCriteriaIsEmpty();
                }
            }

        }

        #endregion

        #region Methods

        protected virtual bool VerifyIsEmpty()
        {
            bool result = string.IsNullOrWhiteSpace(Code) &&
                          string.IsNullOrWhiteSpace(Name) &&
                          Active &&
                          (!CreatedFrom.HasValue || CreatedFrom.Value == DateTime.MinValue) &&
                          (!CreatedTo.HasValue || CreatedTo.Value == DateTime.MinValue) &&
                          (!LastModifyFrom.HasValue || LastModifyFrom.Value == DateTime.MinValue) &&
                          (!LastModifyTo.HasValue || LastModifyTo.Value == DateTime.MinValue) &&
                          string.IsNullOrWhiteSpace(UserModify);
            return result;
        }
        protected virtual void OnSearchCriteriaChanged()
        {
            SearchCriteriaChanged?.Invoke(this, new EventArgs());
            IsEmpty = VerifyIsEmpty();
        }

        protected virtual void OnSearchCriteriaIsEmpty()
        {
            SearchCriteriaIsEmpty?.Invoke(this, new EventArgs());
        }

        public virtual void Clear()
        {
            Code = string.Empty;
            Name = string.Empty;
            Active = true;
            CreatedFrom = null;
            CreatedTo = null;
            LastModifyFrom = null;
            LastModifyTo = null;
            UserModify = string.Empty;
        }

        #endregion

        #region Events

        public event EventHandler SearchCriteriaChanged = delegate { };
        public event EventHandler SearchCriteriaIsEmpty = delegate { };

        #endregion

    }
}
