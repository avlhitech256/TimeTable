using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using System.Linq;
using Common.Data.Notifier;
using Common.Messenger;
using Common.Messenger.Impl;
using DataService.DataService;
using DataService.Model;

namespace DataService.Entity.Faculty
{
    public class FacultyEntity : Notifier, IFacultyEntity
    {
        #region Members

        private long position;
        private Model.Faculty entity;

        #endregion

        #region Constructors

        public FacultyEntity(IDataService dataService, IMessenger messanger)
        {
            DataService = dataService;
            Messenger = messanger;
            position = 0;
            CreateHighSchool();
        }

        public FacultyEntity(IDataService dataService, IMessenger messanger, Model.Faculty entity) 
            : this(dataService, messanger, entity, 0) { }

        public FacultyEntity(IDataService dataService, IMessenger messanger, Model.Faculty entity, long position)
        {
            DataService = dataService;
            Messenger = messanger;
            this.entity = entity;
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
                    Entity.Code = value.ToUpper().Trim();
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

        public long HighSchoolId
        {
            get
            {
                return Entity?.HighSchoolId ?? 0L;
            }

            set
            {
                if (Entity != null && Entity.HighSchoolId != value)
                {
                    Entity.HighSchoolId = value;
                    SetInfoAboutModify();
                    OnPropertyChanged();
                }

            }

        }

        public Model.HighSchool HighSchool
        {
            get
            {
                return Entity?.HighSchool;
            }

            set
            {
                if (Entity != null && Entity.HighSchool != value)
                {
                    Entity.HighSchool = value;
                    SetInfoAboutModify();
                    OnPropertyChanged();
                }

            }

        }

        public ICollection<Chair> Chairs => Entity.Chairs;

        public Model.Faculty Entity
        {
            get
            {
                return entity;
            }

            private set
            {
                if (entity != value)
                {
                    entity = value;
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
                    Model.Faculty newFaculty = DataService?.DBContext?.Faculties?.Create();

                    if (newFaculty != null)
                    {
                        DataService?.DBContext?.Faculties?.Add(newFaculty);
                        Entity = newFaculty;
                        Active = true;
                        Model.HighSchool highSchool = DataService.DBContext.HighSchools.FirstOrDefault();
                        HighSchoolId = highSchool?.Id ?? 0;
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
                OnEntityException(e);
            }
            catch (DbEntityValidationException e)
            {
                OnDbEntityValidationException(e);
            }

        }

        private void SetInfoAboutModify()
        {
            UserModify = DataService?.UserName;
            Entity.LastModify = DateTimeOffset.Now;
            OnPropertyChanged(nameof(LastModify));
        }

        private void OnEntityException(EntityException e)
        {
            Messenger?.Send(CommandName.ShowEntityException, e);
        }

        private void OnDbEntityValidationException(DbEntityValidationException e)
        {
            Messenger?.Send(CommandName.ShowDbEntityValidationException, e);
        }

        #endregion
    }
}
