using System;
using System.Linq;
using Common.Data.Notifier;
using DataService.DataService;
using DataService.Model;

namespace Domain.Entity.HighSchool
{
    public class HighSchoolEntity : Notifier, IHighSchoolEntity
    {
        #region Members

        private long position;
        private DataService.Model.HighSchool highSchool;

        #endregion

        #region Constructors

        public HighSchoolEntity(IDataService dataService)
        {
            DataService = dataService;
            position = 0;
            CreateHighSchool();
        }

        public HighSchoolEntity(IDataService dataService, DataService.Model.HighSchool highSchool) 
            : this(dataService, highSchool, 0) {}

        public HighSchoolEntity(IDataService dataService, DataService.Model.HighSchool highSchool, long position)
        {
            DataService = dataService;
            this.highSchool = highSchool;
            this.position = position;
        }

        #endregion

        #region Properties

        private IDataService DataService { get; }

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
                    HighSchool.Code = value.ToUpper();
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
            DataService.Model.HighSchool newHighSchool = DataService?.DBContext?.HighSchools?.Create();

            if (newHighSchool != null)
            {
                DataService?.DBContext?.HighSchools?.Add(newHighSchool);
                HighSchool = newHighSchool;
                Active = true;
                Rector = DataService?.DBContext?.Employees?.FirstOrDefault()?.Id ?? 0;
                UserModify = DataService?.UserName;
                DateTimeOffset now = DateTimeOffset.Now;
                HighSchool.Created = now;
                OnPropertyChanged(nameof(Created));
                HighSchool.LastModify = now;
                OnPropertyChanged(nameof(LastModify));
            }

        }

        private void SetInfoAboutModify()
        {
            UserModify = DataService?.UserName;
            HighSchool.LastModify = DateTimeOffset.Now;
            OnPropertyChanged(nameof(LastModify));
        }

        #endregion

    }

}
