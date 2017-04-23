using System;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using System.Linq;
using Common.Data.Notifier;
using Common.Messenger;
using Common.Messenger.Impl;
using DataService.DataService;
using DataService.Model;

namespace DataService.Entity.Specialty
{
    class SpecialtyEntity : Notifier, ISpecialtyEntity
    {
        #region Members

        private long position;
        private Model.Specialty entity;

        #endregion

        #region Constructors

        public SpecialtyEntity(IDataService dataService, IMessenger messanger)
        {
            DataService = dataService;
            Messenger = messanger;
            position = 0;
            CreateEntity();
        }

        public SpecialtyEntity(IDataService dataService, IMessenger messanger, Model.Specialty entity) 
            : this(dataService, messanger, entity, 0) { }

        public SpecialtyEntity(IDataService dataService, IMessenger messanger, Model.Specialty entity, long position)
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

        public Model.Specialty Entity
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

        private void CreateEntity()
        {
            try
            {
                if (DataService != null && DataService.DBContext != null &&
                    DataService?.DBContext.HighSchools != null && DataService?.DBContext.Employees != null)
                {
                    Model.Specialty newEntity = DataService?.DBContext?.Specialties?.Create();

                    if (newEntity != null)
                    {
                        DataService?.DBContext?.Specialties?.Add(newEntity);
                        Entity = newEntity;
                        Active = true;
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
