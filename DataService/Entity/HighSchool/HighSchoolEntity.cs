﻿using System;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using Common.Data.Notifier;
using Common.Exception;
using Common.Messenger;
using Common.Messenger.Impl;
using DataService.Constant;
using DataService.DataService;
using DataService.Model;

namespace DataService.Entity.HighSchool
{
    public class HighSchoolEntity : Notifier, IHighSchoolEntity
    {
        #region Members

        private long position;
        private Model.HighSchool entity;
        private bool hasChanges;

        #endregion

        #region Constructors

        public HighSchoolEntity(IDataService dataService, IMessenger messanger)
        {
            DataService = dataService;
            Messenger = messanger;
            position = 0;
            hasChanges = false;
            CreateEntity();
            UpdateHasChanges();
        }

        public HighSchoolEntity(IDataService dataService, IMessenger messanger, Model.HighSchool entity) 
            : this(dataService, messanger, entity, 0) {}

        public HighSchoolEntity(IDataService dataService, IMessenger messanger, Model.HighSchool entity, long position)
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

        public long Rector
        {
            get
            {
                return Entity?.Rector ?? 0L;
            }

            set
            {
                try
                {
                    if (Entity != null && Entity.Rector != value)
                    {
                        if (value <= 0)
                        {
                            StringBuilder message = new StringBuilder();
                            message.Append("Поле  [ Ректор ]  не может содержать значение \"");
                            message.Append(DafaultConstant.DefaultRector);
                            message.AppendFormat("\" со значением Id = {0}.", value);
                            message.AppendLine("Пожалуйста, выберите из списка действующего сотрудника.");
                            throw new BusinessLogicException(message.ToString());
                        }

                        if (DataService?.DBContext?.Employees?.ToList().All(x => x.Id != value) ?? true)
                        {
                            StringBuilder message = new StringBuilder();
                            message.Append("Поле  [ Ректор ]  не может содержать ссылку на сотрудника ");
                            message.AppendFormat("со значением Id = {0}.", value);
                            message.AppendLine("Записи сотрудника с этим Id не существует в базе данных.");
                            message.AppendLine("Возможно, данный сотрудник уже был удален из базы данных");
                            message.AppendLine(" с момента последнего обновления данных.");
                            message.AppendLine("Пожалуйста, выберите из списка действующего сотрудника.");
                            throw new BusinessLogicException(message.ToString());
                        }

                        Entity.Rector = value;
                        SetInfoAboutModify();
                        OnPropertyChanged();
                    }
                }
                catch (BusinessLogicException e)
                {
                    OnBusinessLogicException(e);
                }

            }

        }

        public Model.Employee Employee
        {
            get
            {
                return Entity?.Employee;
            }

            set
            {
                try
                {
                    if (Entity != null && Entity.Employee != value)
                    {
                        if (DataService?.DBContext?.Employees?.ToList().All(x => x != value) ?? true)
                        {
                            StringBuilder message = new StringBuilder();
                            message.Append("Поле  [ Ректор ]  не может содержать ссылку на сотрудника ");
                            message.AppendFormat("со значением Id = {0}, Code = \"{1}\" и Name = \"{2}\".",
                                value.Id, value.Code, value.Name);
                            message.AppendLine("Записи сотрудника с этими данными не существует в базе данных.");
                            message.AppendLine("Возможно, данный сотрудник уже был удален из базы данных");
                            message.AppendLine(" с момента последнего обновления данных.");
                            message.AppendLine("Пожалуйста, выберите из списка действующего сотрудника.");
                            throw new BusinessLogicException(message.ToString());
                        }

                        Entity.Employee = value;
                        SetInfoAboutModify();
                        OnPropertyChanged();
                    }

                }
                catch (BusinessLogicException e)
                {
                    OnBusinessLogicException(e);
                }

            }

        }

        public Model.HighSchool Entity
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
                    UpdateHasChanges();
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
                    DataService?.DBContext.HighSchools != null && DataService?.DBContext.Employees != null)
                {
                    Model.HighSchool newEntity = DataService?.DBContext?.HighSchools?.Create();

                    if (newEntity != null)
                    {
                        DataService?.DBContext?.HighSchools?.Add(newEntity);
                        Entity = newEntity;
                        Active = true;
                        Model.Employee employee = DataService.DBContext.Employees.FirstOrDefault();
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

        protected void OnBusinessLogicException(BusinessLogicException e)
        {
            Messenger.Send(CommandName.ShowBusinessLogicException, e);
        }

        #endregion
    }

}
