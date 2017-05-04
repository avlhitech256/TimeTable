using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using Common.Data.Notifier;
using Common.Messenger;
using Common.Messenger.Impl;
using DataService.DataService;
using DataService.Model;

namespace DataService.Entity.Specialization
{
    public class SpecializationEntity : Notifier, ISpecializationEntity
    {
        #region Members

        private long position;
        private Model.Specialization entity;
        private ObservableCollection<Model.Chair> chairs;
        private bool hasChanges;

        #endregion

        #region Constructors

        public SpecializationEntity(IDataService dataService, IMessenger messanger)
        {
            DataService = dataService;
            Messenger = messanger;
            position = 0;
            hasChanges = false;
            CreateEntity();
            UpdateHasChanges();
        }

        public SpecializationEntity(IDataService dataService, IMessenger messanger, Model.Specialization entity) 
            : this(dataService, messanger, entity, 0) { }

        public SpecializationEntity(IDataService dataService, IMessenger messanger, Model.Specialization entity, long position)
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

        public ObservableCollection<Model.Chair> Chairs
        {
            get
            {
                if (chairs == null)
                {
                    chairs = new ObservableCollection<Model.Chair>();
                    chairs.CollectionChanged += Chairs_CollectionChanged;
                    RefreshChildItems();
                }

                return chairs;
            }

        }

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

        public DateTime Created => Entity?.Created.DateTime ?? DateTime.Now;

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

        public long SpecialtyId
        {
            get
            {
                return Entity?.SpecialtyId ?? 0L;
            }

            set
            {
                if (Entity != null && Entity.SpecialtyId != value)
                {
                    Entity.SpecialtyId = value;
                    SetInfoAboutModify();
                    OnPropertyChanged();
                }

            }

        }

        public Model.Specialty Specialty
        {
            get
            {
                return Entity?.Specialty;
            }

            set
            {
                if (Entity != null && Entity.Specialty != value)
                {
                    Entity.Specialty = value;
                    SetInfoAboutModify();
                    OnPropertyChanged();
                }

            }

        }

        public Model.Specialization Entity
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
                Func<object, bool> statusHasChanges =
                    (x) => DataService?.DBContext?.Entry(x) != null &&
                           DataService.DBContext.Entry(x).State != EntityState.Unchanged;

                HasChanges = statusHasChanges(Entity) || (Entity?.ChairToSpecializations?.Any(statusHasChanges) ?? false);
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
            Chairs.Clear();
            Entity.ChairToSpecializations.Select(x => x.Chair).ToList().ForEach(x => Chairs.Add(x));
        }

        private void CreateEntity()
        {
            try
            {
                if (DataService?.DBContext?.Specializations != null && DataService?.DBContext.Specialties != null)
                {
                    Model.Specialization newEntity = DataService?.DBContext?.Specializations?.Create();

                    if (newEntity != null)
                    {
                        DataService?.DBContext?.Specializations?.Add(newEntity);
                        Entity = newEntity;
                        Active = true;
                        Model.Specialty specialty = DataService.DBContext.Specialties.FirstOrDefault();
                        SpecialtyId = specialty?.Id ?? 0;
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

        private void Chairs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (DataService != null && DataService.DBContext != null &&
                DataService.DBContext.ChairToSpecializations != null)
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:

                        foreach (var item in e.NewItems)
                        {
                            Model.Chair chair = item as Model.Chair;

                            if (chair != null)
                            {
                                ChairToSpecialization relation = DataService.DBContext.ChairToSpecializations.Create();
                                relation.Chair = chair;
                                relation.ChairId = chair.Id;
                                relation.Specialization = Entity;
                                relation.SpecializationId = Entity.Id;
                                relation.Active = true;
                                DateTimeOffset now = DateTimeOffset.Now;
                                relation.Created = now;
                                relation.LastModify = now;
                                relation.UserModify = DataService?.UserName;
                            }

                        }

                        break;

                    case NotifyCollectionChangedAction.Remove:

                        foreach (var item in e.OldItems)
                        {
                            Model.Chair chair = item as Model.Chair;

                            if (chair != null)
                            {
                                ChairToSpecialization removedItem =
                                    Entity.ChairToSpecializations.FirstOrDefault(x => x.Chair == chair);

                                if (removedItem != null)
                                {
                                    Entity.ChairToSpecializations.Remove(removedItem);
                                }

                            }

                        }

                        break;

                    case NotifyCollectionChangedAction.Replace:

                        for (int index = 0; index < e.NewItems.Count; index++)
                        {
                            Model.Chair oldChair = e.OldItems[index] as Model.Chair;
                            Model.Chair newChair = e.NewItems[index] as Model.Chair;

                            if (oldChair != null && newChair != null)
                            {
                                ChairToSpecialization replacedItem =
                                    Entity.ChairToSpecializations.FirstOrDefault(x => x.Chair == oldChair);
                                if (replacedItem != null)
                                {
                                    replacedItem.Chair = newChair;
                                    replacedItem.ChairId = newChair.Id;
                                    replacedItem.LastModify = DateTimeOffset.Now;
                                    replacedItem.UserModify = DataService?.UserName;
                                }

                            }

                        }

                        break;

                    case NotifyCollectionChangedAction.Reset:
                        Entity.ChairToSpecializations.Clear();
                        break;
                }

            }

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
