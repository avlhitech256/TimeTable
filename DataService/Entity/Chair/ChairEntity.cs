using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using System.Linq;
using Common.Data.Notifier;
using Common.Messenger;
using Common.Messenger.Impl;
using DataService.DataService;
using DataService.Model;

namespace DataService.Entity.Chair
{
    public class ChairEntity : Notifier, IChairEntity
    {
        #region Members

        private long position;
        private Model.Chair entity;
        private ObservableCollection<Specialization> specializations;
        private bool hasChanges;

        #endregion

        #region Constructors

        public ChairEntity(IDataService dataService, IMessenger messanger)
        {
            DataService = dataService;
            Messenger = messanger;
            position = 0;
            CreateEntity();
            SetHasChanges();
        }

        public ChairEntity(IDataService dataService, IMessenger messanger, Model.Chair entity) 
            : this(dataService, messanger, entity, 0) { }

        public ChairEntity(IDataService dataService, IMessenger messanger, Model.Chair entity, long position)
        {
            DataService = dataService;
            Messenger = messanger;
            this.entity = entity;
            this.position = position;
            SetHasChanges();
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

        public long FacultyId
        {
            get
            {
                return Entity?.FacultyId ?? 0L;
            }

            set
            {
                if (Entity != null && Entity.FacultyId != value)
                {
                    Entity.FacultyId = value;
                    SetInfoAboutModify();
                    OnPropertyChanged();
                }

            }

        }

        public Model.Faculty Faculty
        {
            get
            {
                return Entity?.Faculty;
            }

            set
            {
                if (Entity != null && Entity.Faculty != value)
                {
                    Entity.Faculty = value;
                    SetInfoAboutModify();
                    OnPropertyChanged();
                }

            }

        }

        public ICollection<Model.Specialization> Specializations
        {
            get
            {
                if (specializations == null)
                {
                    specializations = new ObservableCollection<Specialization>();
                    Entity.ChairToSpecializations.Select(x => x.Specialization).ToList().ForEach(x => specializations.Add(x));
                    specializations.CollectionChanged += Specializations_CollectionChanged;
                }

                return specializations;
            }
        }

        private void Specializations_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (DataService != null && DataService.DBContext != null &&
                DataService.DBContext.ChairToSpecializations != null)
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:

                        foreach (var item in e.NewItems)
                        {
                            Specialization spec = item as Specialization;

                            if (spec != null)
                            {
                                ChairToSpecialization relation = DataService.DBContext.ChairToSpecializations.Create();
                                relation.Specialization = spec;
                                relation.SpecializationId = spec.Id;
                                relation.Chair = Entity;
                                relation.ChairId = Entity.Id;
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
                            Specialization spec = item as Specialization;

                            if (spec != null)
                            {
                                ChairToSpecialization removedItem =
                                    Entity.ChairToSpecializations.FirstOrDefault(x => x.Specialization == spec);

                                if (removedItem != null)
                                {
                                    Entity.ChairToSpecializations.Remove(removedItem);
                                }

                            }

                        }

                        break;

                    case NotifyCollectionChangedAction.Replace:

                        for(int index = 0; index < e.NewItems.Count; index++)
                        {
                            Specialization oldSpec = e.OldItems[index] as Specialization;
                            Specialization newSpec = e.NewItems[index] as Specialization;

                            if (oldSpec != null && newSpec != null)
                            {
                                ChairToSpecialization replacedItem =
                                    Entity.ChairToSpecializations.FirstOrDefault(x => x.Specialization == oldSpec);
                                if (replacedItem != null)
                                {
                                    replacedItem.Specialization = newSpec;
                                    replacedItem.SpecializationId = newSpec.Id;
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

            SetHasChanges();
        }

        public Model.Chair Entity
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

        private void SetHasChanges()
        {
            try
            {
                Func<object, bool> validRelation =
                    (x) => DataService?.DBContext?.Entry(x) != null &&
                           DataService.DBContext.Entry(x).State != EntityState.Unchanged; 
                
                HasChanges = validRelation(Entity) || (Entity?.ChairToSpecializations?.Any(validRelation) ?? false);
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

        private void CreateEntity()
        {
            try
            {
                if (DataService != null && DataService.DBContext != null &&
                    DataService?.DBContext.Chairs != null && DataService?.DBContext.Faculties != null)
                {
                    Model.Chair newEntity = DataService?.DBContext?.Chairs?.Create();

                    if (newEntity != null)
                    {
                        DataService?.DBContext?.Chairs?.Add(newEntity);
                        Entity = newEntity;
                        Active = true;
                        Model.Faculty faculty = DataService.DBContext.Faculties.FirstOrDefault();
                        FacultyId = faculty?.Id ?? 0;
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
            SetHasChanges();
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
