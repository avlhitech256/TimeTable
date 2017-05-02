using System;
using System.Collections.Generic;
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

namespace DataService.Entity.Faculty
{
    public class FacultyEntity : Notifier, IFacultyEntity
    {
        #region Members

        private long position;
        private Model.Faculty entity;
        private bool hasChanges;

        #endregion

        #region Constructors

        public FacultyEntity(IDataService dataService, IMessenger messanger)
        {
            DataService = dataService;
            Messenger = messanger;
            position = 0;
            hasChanges = false;
            CreateEntity();
            UpdateHasChanges();
        }

        public FacultyEntity(IDataService dataService, IMessenger messanger, Model.Faculty entity) 
            : this(dataService, messanger, entity, 0) { }

        public FacultyEntity(IDataService dataService, IMessenger messanger, Model.Faculty entity, long position)
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

        public long HighSchoolId
        {
            get
            {
                return Entity?.HighSchoolId ?? 0L;
            }

            set
            {
                try
                {
                    if (Entity != null && Entity.HighSchoolId != value)
                    {
                        if (value <= 0)
                        {
                            StringBuilder message = new StringBuilder();
                            message.Append("Поле  [ ВУЗ ]  не может содержать значение \"");
                            message.Append(DafaultConstant.DefaultHighSchool);
                            message.AppendFormat("\" со значением Id = {0}.", value);
                            message.AppendLine("Пожалуйста, выберите из списка действующих учебных заведений.");
                            throw new BusinessLogicException(message.ToString());
                        }

                        if (DataService?.DBContext?.HighSchools?.ToList().All(x => x.Id != value) ?? true)
                        {
                            StringBuilder message = new StringBuilder();
                            message.Append("Поле  [ ВУЗ ]  не может содержать ссылку на учебное заведение ");
                            message.AppendFormat("со значением Id = {0}.", value);
                            message.AppendLine("Записи ВУЗа с этим Id не существует в базе данных.");
                            message.AppendLine("Возможно, данное учебное заведение уже было удалено из базы данных");
                            message.AppendLine(" с момента последнего обновления данных.");
                            message.AppendLine("Пожалуйста, выберите из списка действующий ВУЗ.");
                            throw new BusinessLogicException(message.ToString());
                        }

                        Entity.HighSchoolId = value;
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

        public Model.HighSchool HighSchool
        {
            get
            {
                return Entity?.HighSchool;
            }

            set
            {
                try
                {
                    if (Entity != null && Entity.HighSchool != value)
                    {
                        if (DataService?.DBContext?.HighSchools?.ToList().All(x => x != value) ?? true)
                        {
                            StringBuilder message = new StringBuilder();
                            message.Append("Поле  [ ВУЗ ]  не может содержать ссылку на учебное заведение ");
                            message.AppendFormat("со значением Id = {0}, Code = \"{1}\" и Name = \"{2}\".",
                                value.Id, value.Code, value.Name);
                            message.AppendLine("Записи ВУЗа с этими данными не существует в базе данных.");
                            message.AppendLine("Возможно, данное учебное заведение уже было удалено из базы данных");
                            message.AppendLine(" с момента последнего обновления данных.");
                            message.AppendLine("Пожалуйста, выберите из списка действующий ВУЗ.");
                            throw new BusinessLogicException(message.ToString());
                        }

                        Entity.HighSchool = value;
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

        public ICollection<Model.Chair> Chairs => Entity.Chairs;

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
                    DataService?.DBContext.Faculties != null && DataService?.DBContext.HighSchools != null)
                {
                    Model.Faculty newEntity = DataService?.DBContext?.Faculties?.Create();

                    if (newEntity != null)
                    {
                        DataService?.DBContext?.Faculties?.Add(newEntity);
                        Entity = newEntity;
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
