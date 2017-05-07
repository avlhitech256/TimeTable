#region FileInfo
// TimeTable, DataService
// EmployeeEntity.cs
// Ilya Likhoshva
// Created: 07.05.2017 12:51
#endregion

using System;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using Common.Data.Notifier;
using Common.Messenger;
using Common.Messenger.Impl;
using DataService.DataService;

namespace DataService.Entity.Employee
{
    public class EmployeeEntity : Notifier, IEmployeeEntity
    {
        #region Members

        private long position;
        private Model.Employee entity;
        private bool hasChanges;

        #endregion

        #region Constructors

        public EmployeeEntity(IDataService dataService, IMessenger messanger)
        {
            DataService = dataService;
            Messenger = messanger;
            position = 0;
            hasChanges = false;
            CreateEntity();
            UpdateHasChanges();
        }

        public EmployeeEntity(IDataService dataService, IMessenger messanger, Model.Employee entity) 
            : this(dataService, messanger, entity, 0) { }

        public EmployeeEntity(IDataService dataService, IMessenger messanger, Model.Employee entity, long position)
        {
            DataService = dataService;
            Messenger = messanger;
            this.entity = entity;
            this.position = position;
            hasChanges = false;
            UpdateHasChanges();
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

        public Model.Employee Entity
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

        public bool HasChanges
        {
            get
            {
                return hasChanges;
            }

            private set
            {
                if (hasChanges != value)
                {
                    hasChanges = value;
                    OnPropertyChanged();
                }
            }

        }

        #endregion

        #region Methods

        public void UpdateHasChanges()
        {
            try
            {
                HasChanges = DataService?.DBContext?.Entry(Entity) != null &&
                             DataService.DBContext.Entry(Entity).State != EntityState.Unchanged;
            }
            catch (EntityException e)
            {
                OnEntityException(e);
            }
            catch (DbEntityValidationException e)
            {
                OnDbEntityValidationException(e);
            }
            catch (DbUpdateException e)
            {
                OnDbUpdateException(e);
            }

        }

        public void RefreshChildItems()
        {
            throw new NotImplementedException();
        }

        private void CreateEntity()
        {
            try
            {
                if (DataService != null && DataService.DBContext != null &&
                    DataService?.DBContext.Specialties != null)
                {
                    Model.Employee newEntity = DataService?.DBContext?.Employees?.Create();

                    if (newEntity != null)
                    {
                        DataService?.DBContext?.Employees?.Add(newEntity);
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
            catch (DbUpdateException e)
            {
                OnDbUpdateException(e);
            }

        }

        private void SetInfoAboutModify()
        {
            UserModify = DataService?.UserName;
            Entity.LastModify = DateTimeOffset.Now;
            OnPropertyChanged(nameof(LastModify));
            UpdateHasChanges();
        }

        protected void OnEntityException(EntityException e)
        {
            Messenger?.Send(CommandName.ShowEntityException, e);
        }

        protected void OnDbEntityValidationException(DbEntityValidationException e)
        {
            Messenger?.Send(CommandName.ShowDbEntityValidationException, e);
        }

        protected void OnDbUpdateException(DbUpdateException e)
        {
            Messenger?.Send(CommandName.ShowDbUpdateException, e);
        }

        #endregion
    }
}