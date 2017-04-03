using System;
//using Common.Data.Notifier;

namespace DataService.Entity.HighSchool
{
    public class HighSchoolEntity //: Notifier, IHighSchoolEntity
    {
        #region Members

        private readonly Model.HighSchool highSchool;

        #endregion

        #region Constructors

        public HighSchoolEntity(Model.HighSchool highSchool)
        {
            this.highSchool = highSchool;
        }

        #endregion

        #region Properties

        public long Id
        {
            get
            {
                return highSchool.Id;
            }

            set
            {
                if (highSchool.Id != value)
                {
                    highSchool.Id = value;
                    //OnPropertyChanged();
                }

            }

        }

        public string Code
        {
            get
            {
                return highSchool.Code;
            }

            set
            {
                if (highSchool.Code != value)
                {
                    highSchool.Code = value;
                    //OnPropertyChanged();
                }
            }

        }

        public string Name
        {
            get
            {
                return highSchool.Name;
            }

            set
            {
                if (highSchool.Name != value)
                {
                    highSchool.Name = value;
                    //OnPropertyChanged();
                }

            }
        }

        public bool Active
        {
            get
            {
                return highSchool.Active;
            }

            set
            {
                if (highSchool.Active != value)
                {
                    highSchool.Active = value;
                    //OnPropertyChanged();
                }

            }

        }

        public DateTimeOffset Cteated
        {
            get
            {
                return highSchool.Cteated;
            }

            set
            {
                if (highSchool.Cteated != value)
                {
                    highSchool.Cteated = value;
                    //OnPropertyChanged();
                }
            }
        }

        public DateTimeOffset LastModify
        {
            get
            {
                return highSchool.LastModify;
            }

            set
            {
                if (highSchool.LastModify != value)
                {
                    highSchool.LastModify = value;
                    //OnPropertyChanged();
                }
            }
        }

        public string UserModify
        {
            get
            {
                return highSchool.UserModify;
            }

            set
            {
                if (highSchool.UserModify != value)
                {
                    highSchool.UserModify = value;
                    //OnPropertyChanged();
                }

            }
        }


        #endregion

        #region Methods
        #endregion
    }
}
