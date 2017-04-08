using System;
using Common.Data.Notifier;
using DataService.Model;
using Domain.DomainContext;

namespace Domain.Entity.HighSchool
{
    public class HighSchoolEntity : Notifier, IHighSchoolEntity
    {
        #region Members

        private long position;
        private DataService.Model.HighSchool highSchool;

        #endregion

        #region Constructors

        public HighSchoolEntity(IDomainContext domainContext)
        {
            DomainContext = domainContext;
            position = 0;
            CreateHighSchool();
        }

        public HighSchoolEntity(IDomainContext domainContext, DataService.Model.HighSchool highSchool) 
            : this(domainContext, highSchool, 0) {}

        public HighSchoolEntity(IDomainContext domainContext, DataService.Model.HighSchool highSchool, long position)
        {
            DomainContext = domainContext;
            this.highSchool = highSchool;
            this.position = position;
        }

        #endregion

        #region Properties

        private IDomainContext DomainContext { get; }

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
                return HighSchool.Id;
            }

            set
            {
                if (HighSchool.Id != value)
                {
                    HighSchool.Id = value;
                    OnPropertyChanged();
                }

            }

        }

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

        public DateTimeOffset Created
        {
            get
            {
                return HighSchool.Cteated;
            }

            private set
            {
                if (HighSchool.Cteated != value)
                {
                    HighSchool.Cteated = value;
                    OnPropertyChanged();
                }

            }

        }

        public DateTimeOffset LastModify
        {
            get
            {
                return HighSchool.LastModify;
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
            DataService.Model.HighSchool newHighSchool = DomainContext?.DataService?.DBContext?.HighSchools?.Create();

            if (newHighSchool != null)
            {
                DomainContext?.DataService?.DBContext?.HighSchools?.Add(newHighSchool);
                HighSchool = newHighSchool;
                UserModify = DomainContext?.UserName;
                DateTimeOffset now = DateTimeOffset.Now;
                Created = now;
                LastModify = now;
            }

        }

        private void SetInfoAboutModify()
        {
            UserModify = DomainContext?.UserName;
            LastModify = DateTimeOffset.Now;
        }

        #endregion

    }

}
