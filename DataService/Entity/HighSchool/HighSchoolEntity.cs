using System;
using System.ComponentModel;
using Common.Data.Notifier;
using DataService.Model;

namespace DataService.Entity.HighSchool
{
    public class HighSchoolEntity : Notifier, IHighSchoolEntity
    {
        #region Members

        private Model.HighSchool highSchool;
        private long position;

        #endregion

        #region Constructors

        public HighSchoolEntity(Model.HighSchool highSchool) : this(highSchool, 0) {}

        public HighSchoolEntity(Model.HighSchool highSchool, long position)
        {
            this.highSchool = highSchool;
            this.position = position;
        }

        #endregion

        #region Properties

        public long Position
        {
            get
            {
                return position;
            }

            set
            {
                if (position != value)
                {
                    position = value;
                    OnPropertyChanged();
                }

            }

        }
        
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
                }

            }

        }

        public long Rector
        {
            get
            {
                return highSchool.Rector;
            }

            set
            {
                if (highSchool.Rector != value)
                {
                    highSchool.Rector = value;
                    OnPropertyChanged();
                }

            }

        }

        public Employee Employee
        {
            get
            {
                return highSchool.Employee;
            }

            set
            {
                if (highSchool.Employee != value)
                {
                    highSchool.Employee = value;
                    OnPropertyChanged();
                }

            }

        }

        public Model.HighSchool HighSchool
        {
            get
            {
                return highSchool;
            }

            set
            {
                if (highSchool != value)
                {
                    highSchool = value;
                    OnPropertyChanged();
                }

            }

        }


        #endregion

    }

}
