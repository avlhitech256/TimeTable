using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.Criteria
{
    public class SearchCriteria : Notifier.Notifier
    {
        #region Members

        private string code;
        private string name;
        private bool active;
        private DateTimeOffset cteatedFrom;
        private DateTimeOffset cteatedTo;
        private DateTimeOffset lastModifyFrom;
        private DateTimeOffset lastModifyTo;
        private string userModify;

        #endregion

        #region Constructors

        public SearchCriteria()
        {
            Code = string.Empty;
            Name = string.Empty;
            Active = true;
            CteatedFrom = DateTimeOffset.Now.Date.AddYears(-1);
            CteatedTo = DateTimeOffset.Now.Date;
            LastModifyFrom = DateTimeOffset.Now.Date.AddYears(-1);
            LastModifyTo = DateTimeOffset.Now.Date.AddYears(-1);
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
                    code = value;
                    OnPropertyChanged();
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
                }

            }

        }

        public DateTimeOffset CteatedFrom
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
                }
            }
        }

        public DateTimeOffset CteatedTo
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
                }
            }
        }

        public DateTimeOffset LastModifyFrom
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
                }
            }
        }

        public DateTimeOffset LastModifyTo
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
                }

            }
        }


        #endregion

    }
}
