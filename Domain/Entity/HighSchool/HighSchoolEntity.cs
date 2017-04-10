using System;
using Common.Data.Notifier;
using DataService.DataService;
using DataService.Model;
using Domain.DomainContext;

namespace Domain.Entity.HighSchool
{
    public class HighSchoolEntity : Notifier, IHighSchoolEntity
    {
        #region Members

        private long position;
        private DataService.Model.HighSchool highSchool;
        private readonly string userName;

        #endregion

        #region Constructors

        public HighSchoolEntity(TimeTableEntities dbContext, string userName)
        {
            DbContext = dbContext;
            this.userName = userName;
            position = 0;
            CreateHighSchool();
        }

        public HighSchoolEntity(TimeTableEntities dbContext, string userName, DataService.Model.HighSchool highSchool) 
            : this(dbContext, userName, highSchool, 0) {}

        public HighSchoolEntity(TimeTableEntities dbContext, string userName, DataService.Model.HighSchool highSchool, long position)
        {
            DbContext = dbContext;
            this.userName = userName;
            this.highSchool = highSchool;
            this.position = position;
        }

        #endregion

        #region Properties

        private TimeTableEntities DbContext { get; }

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
        
        public long Id => HighSchool.Id;

        public string Code
        {
            get
            {
                return HighSchool.Code;
            }

            set
            {
                if (HighSchool.Code != value)
                {
                    HighSchool.Code = value;
                    OnPropertyChanged();
                }

            }

        }

        public string Name
        {
            get
            {
                return HighSchool.Name;
            }

            set
            {
                if (HighSchool.Name != value)
                {
                    HighSchool.Name = value;
                    OnPropertyChanged();
                }

            }

        }

        public bool Active
        {
            get
            {
                return HighSchool.Active;
            }

            set
            {
                if (HighSchool.Active != value)
                {
                    HighSchool.Active = value;
                    OnPropertyChanged();
                }

            }

        }

        public DateTime Created
        {
            get
            {
                return HighSchool.Created.DateTime;
            }

            private set
            {
                if (HighSchool.Created != value)
                {
                    HighSchool.Created = value;
                    OnPropertyChanged();
                }

            }

        }

        public DateTime LastModify
        {
            get
            {
                return HighSchool.LastModify.DateTime;
            }

            private set
            {
                if (HighSchool.LastModify != value)
                {
                    HighSchool.LastModify = value;
                    OnPropertyChanged();
                }

            }

        }

        public string UserModify
        {
            get
            {
                return HighSchool.UserModify;
            }

            private set
            {
                if (HighSchool.UserModify != value)
                {
                    HighSchool.UserModify = value;
                    SetInfoAboutModify();
                    OnPropertyChanged();
                }

            }

        }

        public long Rector
        {
            get
            {
                return HighSchool.Rector;
            }

            set
            {
                if (HighSchool.Rector != value)
                {
                    HighSchool.Rector = value;
                    SetInfoAboutModify();
                    OnPropertyChanged();
                }

            }

        }

        public Employee Employee
        {
            get
            {
                return HighSchool.Employee;
            }

            set
            {
                if (HighSchool.Employee != value)
                {
                    HighSchool.Employee = value;
                    SetInfoAboutModify();
                    OnPropertyChanged();
                }

            }

        }

        public DataService.Model.HighSchool HighSchool
        {
            get
            {
                return highSchool;
            }

            private set
            {
                if (highSchool != value)
                {
                    highSchool = value;
                    OnPropertyChanged();
                }

            }

        }

        #endregion

        #region Methods

        private void CreateHighSchool()
        {
            DataService.Model.HighSchool newHighSchool = DbContext?.HighSchools?.Create();

            if (newHighSchool != null)
            {
                DbContext?.HighSchools?.Add(newHighSchool);
                HighSchool = newHighSchool;
                UserModify = userName;
                DateTimeOffset now = DateTimeOffset.Now;
                HighSchool.Created = now;
                OnPropertyChanged(nameof(Created));
                HighSchool.LastModify = now;
                OnPropertyChanged(nameof(LastModify));
            }

        }

        private void SetInfoAboutModify()
        {
            UserModify = userName;
            HighSchool.LastModify = DateTimeOffset.Now;
            OnPropertyChanged(nameof(LastModify));
        }

        #endregion

    }

}
