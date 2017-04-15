using System;
using System.Data.Entity.Core;
using System.Linq;
using Domain.Data.Notifier;
using Domain.Messenger;
using Domain.Messenger.Impl;
using DataService.DataService;
using DataService.Model;

namespace Domain.Entity.HighSchool
{
    public class HighSchoolEntity : Notifier, IHighSchoolEntity
    {
        #region Members

        private long position;
        private DataService.Model.HighSchool _entity;

        #endregion

        #region Constructors

        public HighSchoolEntity(IDataService dataService, IMessenger messanger)
        {
            DataService = dataService;
            Messenger = messanger;
            position = 0;
            CreateHighSchool();
        }

        public HighSchoolEntity(IDataService dataService, IMessenger messanger, DataService.Model.HighSchool _entity) 
            : this(dataService, messanger, _entity, 0) {}

        public HighSchoolEntity(IDataService dataService, IMessenger messanger, DataService.Model.HighSchool _entity, long position)
        {
            DataService = dataService;
            Messenger = messanger;
            this._entity = _entity;
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
        
        public long Id => Entity.Id;

        public string Code
        {
            get
            {
                return Entity?.Code;
            }

            set
            {
                if (Entity != null && Entity.Code != value)
                {
                    Entity.Code = value.ToUpper();
                    SetInfoAboutModify();
                    OnPropertyChanged();
                }

            }

        }

        public string Name
        {
            get
            {
                return Entity?.Name;
            }

            set
            {
                if (Entity != null && Entity.Name != value)
                {
                    Entity.Name = value;
                    SetInfoAboutModify();
                    OnPropertyChanged();
                }

            }

        }

        public bool Active
        {
            get
            {
                return Entity?.Active ?? true;
            }

            set
            {
                if (Entity != null && Entity.Active != value)
                {
                    Entity.Active = value;
                    SetInfoAboutModify();
                    OnPropertyChanged();
                }

            }

        }

        public DateTime Created
        {
            get
            {
                return Entity?.Created.DateTime ?? DateTime.Now;
            }

            private set
            {
                if (Entity != null && Entity.Created != value)
                {
                    Entity.Created = value;
                    OnPropertyChanged();
                }

            }

        }

        public DateTime LastModify => Entity?.LastModify.DateTime ?? DateTime.Now;

        public string UserModify
        {
            get
            {
                return Entity?.UserModify;
            }

            private set
            {
                if (Entity != null && Entity.UserModify != value)
                {
                    Entity.UserModify = value;
                    OnPropertyChanged();
                }

            }

        }

        public long Rector
        {
            get
            {
                return Entity?.Rector ?? 0L;
            }

            set
            {
                if (Entity != null && Entity.Rector != value)
                {
                    Entity.Rector = value;
                    SetInfoAboutModify();
                    OnPropertyChanged();
                }

            }

        }

        public Employee Employee
        {
            get
            {
                return Entity?.Employee;
            }

            set
            {
                if (Entity != null && Entity.Employee != value)
                {
                    Entity.Employee = value;
                    SetInfoAboutModify();
                    OnPropertyChanged();
                }

            }

        }

        public DataService.Model.HighSchool Entity
        {
            get
            {
                return _entity;
            }

            private set
            {
                if (_entity != value)
                {
                    _entity = value;
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
                        Entity = newHighSchool;
                        Active = true;
                        Employee employee = DataService.DBContext.Employees.FirstOrDefault();
                        Rector = employee?.Id ?? 0;
                        UserModify = DataService?.UserName;
                        DateTimeOffset now = DateTimeOffset.Now;
                        Entity.Created = now;
                        OnPropertyChanged(nameof(Created));
                        Entity.LastModify = now;
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
            Entity.LastModify = DateTimeOffset.Now;
            OnPropertyChanged(nameof(LastModify));
        }

        #endregion

    }

}
