using System;
using System.Data.Entity.Core;
using System.Linq;
using Common.Data.Notifier;
using DataService.DataService;
using DataService.Model;
using Domain.Messenger;
using Domain.Messenger.Impl;

namespace Domain.Entity.HighSchool
{
    public class HighSchoolEntity : Notifier, IHighSchoolEntity
    {
        #region Members

        private long position;
        private DataService.Model.HighSchool highSchool;

        #endregion

        #region Constructors

        public HighSchoolEntity(IDataService dataService, IMessenger messanger)
        {
            DataService = dataService;
            Messenger = messanger;
            position = 0;
            CreateHighSchool();
        }

        public HighSchoolEntity(IDataService dataService, IMessenger messanger, DataService.Model.HighSchool highSchool) 
            : this(dataService, messanger, highSchool, 0) {}

        public HighSchoolEntity(IDataService dataService, IMessenger messanger, DataService.Model.HighSchool highSchool, long position)
        {
            DataService = dataService;
            Messenger = messanger;
            this.highSchool = highSchool;
            this.position = position;
        }

        #endregion

        #region Properties

        private IDataService DataService { get; }

        private IMessenger Messenger { get; }

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
                return HighSchool?.Code;
            }

            set
            {
                if (HighSchool != null && HighSchool.Code != value)
                {
                    HighSchool.Code = value.ToUpper();
                    SetInfoAboutModify();
                    OnPropertyChanged();
                }

            }

        }

        public string Name
        {
            get
            {
                return HighSchool?.Name;
            }

            set
            {
                if (HighSchool != null && HighSchool.Name != value)
                {
                    HighSchool.Name = value;
                    SetInfoAboutModify();
                    OnPropertyChanged();
                }

            }

        }

        public bool Active
        {
            get
            {
                return HighSchool?.Active ?? true;
            }

            set
            {
                if (HighSchool != null && HighSchool.Active != value)
                {
                    HighSchool.Active = value;
                    SetInfoAboutModify();
                    OnPropertyChanged();
                }

            }

        }

        public DateTime Created
        {
            get
            {
                return HighSchool?.Created.DateTime ?? DateTime.Now;
            }

            private set
            {
                if (HighSchool != null && HighSchool.Created != value)
                {
                    HighSchool.Created = value;
                    OnPropertyChanged();
                }

            }

        }

        public DateTime LastModify => HighSchool?.LastModify.DateTime ?? DateTime.Now;

        public string UserModify
        {
            get
            {
                return HighSchool?.UserModify;
            }

            private set
            {
                if (HighSchool != null && HighSchool.UserModify != value)
                {
                    HighSchool.UserModify = value;
                    OnPropertyChanged();
                }

            }

        }

        public long Rector
        {
            get
            {
                return HighSchool?.Rector ?? 0L;
            }

            set
            {
                if (HighSchool != null && HighSchool.Rector != value)
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
                return HighSchool?.Employee;
            }

            set
            {
                if (HighSchool != null && HighSchool.Employee != value)
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
            try
            {
                if (DataService != null && DataService.DBContext != null && 
                    DataService?.DBContext.HighSchools != null && DataService?.DBContext.Employees != null)
                {
                    DataService.Model.HighSchool newHighSchool = DataService?.DBContext?.HighSchools?.Create();

                    if (newHighSchool != null)
                    {
                        DataService?.DBContext?.HighSchools?.Add(newHighSchool);
                        HighSchool = newHighSchool;
                        Active = true;
                        Employee employee = DataService.DBContext.Employees.FirstOrDefault();
                        Rector = employee?.Id ?? 0;
                        UserModify = DataService?.UserName;
                        DateTimeOffset now = DateTimeOffset.Now;
                        HighSchool.Created = now;
                        OnPropertyChanged(nameof(Created));
                        HighSchool.LastModify = now;
                        OnPropertyChanged(nameof(LastModify));
                    }

                }

            }
            catch (EntityException e)
            {
                Messenger.Send(CommandName.ShowEntityException, e);
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
