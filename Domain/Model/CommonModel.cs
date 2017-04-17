using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Common.Data.Notifier;
using Common.DomainContext;
using Common.Event;
using Common.Messenger;
using DataService.DataService;
using DataService.Entity;
using DataService.Model;
using Domain.SearchCriteria;

namespace Domain.Model
{
    public class CommonModel<T> : Notifier, ICommonModel<T> where T : class
    {
        #region Members

        private IDomainEntity<T> selectedItem;
        private ObservableCollection<IDomainEntity<T>> entities;
        private bool entitiesIsLoaded;

        #endregion

        #region Constructors

        protected CommonModel(IDomainContext domainContext)
        {
            DomainContext = domainContext;
            entitiesIsLoaded = false;
            InitializeDataService();
            InitializeSearchCriteria();
        }

        #endregion

        #region Properties

        public SearchCriteria.ISearchCriteria SearchCriteria { get; private set; }

        public IDomainEntity<T> SelectedItem
        {
            get
            {
                return selectedItem;
            }

            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    OnPropertyChanged();
                }

            }

        }

        public ObservableCollection<IDomainEntity<T>> Entities
        {
            get
            {
                if (!entitiesIsLoaded && entities == null)
                {
                    InitializeEntities();
                    entitiesIsLoaded = true;
                }

                return entities;
            }

            private set
            {
                if (entities != value)
                {
                    entities = value;
                    OnPropertyChanged();
                }

            }

        }

        public bool HasChanges
        {
            get
            {
                bool hasChanges = false;

                try
                {
                    hasChanges = SelectedItem != null && DbContext?.Entry(SelectedItem.Entity)?.State != EntityState.Unchanged;
                }
                catch (EntityException e)
                {
                    OnEntityException(e);
                }

                return hasChanges;
            }

        }

        public string DataBaseServer
        {
            get
            {
                string dataBaseServer = string.Empty;

                try
                {
                    dataBaseServer = DbContext?.Database?.Connection?.DataSource;
                }
                catch (EntityException e)
                {
                    OnEntityException(e);
                }

                return dataBaseServer;
            }

        }

        protected IDomainContext DomainContext { get; }

        protected IMessenger Messenger => DomainContext?.Messenger;

        protected IDataService DataService { get; set; }

        protected TimeTableEntities DbContext => DataService?.DBContext;

        #endregion

        #region Methods

        private void InitializeDataService()
        {
            try
            {
                DataService = new DataService.DataService.DataService
                {
                    UserName = DomainContext.UserName
                };

            }
            catch (EntityException e)
            {
                OnEntityException(e);
            }

        }

        private void InitializeSearchCriteria()
        {
            SearchCriteria = new SearchCriteriaFactory().GetSearchCriteria(typeof(T));
            SearchCriteria.Code = string.Empty;
            SearchCriteria.Name = string.Empty;
            SearchCriteria.Active = true;
            SearchCriteria.CteatedTo = null;
            SearchCriteria.CteatedFrom = null;
            SearchCriteria.LastModifyTo = null;
            SearchCriteria.LastModifyFrom = null;
            SearchCriteria.UserModify = string.Empty;
        }

        private void InitializeEntities()
        {
            var domainEntities = new ObservableCollection<IDomainEntity<T>>();
            long position = 1;
            IDomainEntityFactory factory = new DomainEntityFactory(DataService, Messenger);

            try
            {
                foreach (T item in DbContext.Set<T>().ToList())
                {
                    domainEntities.Add(factory.GetDomainEntity(item, position));
                }

                Entities = domainEntities;
            }
            catch (EntityException e)
            {
                OnEntityException(e);
            }

        }

        protected virtual List<T> SelectEntities()
        {
            List<T> selectedEntities = DbContext.Set<T>().ToList();

            return selectedEntities;
        }

        public void ApplySearchCriteria()
        {
            Entities.Clear();
            long position = 1;
            IDomainEntityFactory factory = new DomainEntityFactory(DataService, Messenger);

            try
            {
                foreach (T item in SelectEntities())
                {
                    Entities.Add(factory.GetDomainEntity(item, position));
                }

                OnPropertyChanged(nameof(Entities));
            }
            catch (EntityException e)
            {
                OnEntityException(e);
            }

        }

        public void Add()
        {
            IDomainEntityFactory factory = new DomainEntityFactory(DataService, Messenger);
            SelectedItem = factory.GetDomainEntity<T>();
        }

        public void Rollback()
        {
            try
            {
                if (DbContext != null)
                {
                    DbEntityEntry entry = DbContext.Entry(SelectedItem.Entity);

                    if (entry != null)
                    {
                        switch (entry.State)
                        {
                            case EntityState.Modified:
                                entry.State = EntityState.Unchanged;
                                break;
                            case EntityState.Deleted:
                                entry.Reload();
                                break;
                            case EntityState.Added:
                                entry.State = EntityState.Detached;
                                SelectedItem = null;
                                break;
                        }

                    }

                    Save();
                }

            }
            catch (EntityException e)
            {
                OnEntityException(e);
            }

        }

        public bool ValidateRequiredCode()
        {
            return string.IsNullOrWhiteSpace(SelectedItem.Code);
        }

        public bool ValidateUniqueCode()
        {
            return DbContext.HighSchools.ToList().All(x => x.Code != SelectedItem.Code);
        }

        public void Save()
        {
            try
            {
                if (HasChanges)
                {
                    DbContext.SaveChanges();
                }

            }
            catch (EntityException e)
            {
                OnEntityException(e);
            }

        }

        public void Delete()
        {
            try
            {
                if (SelectedItem != null)
                {
                    DbContext.Set<T>().Remove(SelectedItem.Entity);
                    Save();
                    SelectedItem = null;
                }

            }
            catch (EntityException e)
            {
                OnEntityException(e);
            }

        }

        protected void OnEntityException(EntityException e)
        {
            EntityException?.Invoke(this, new EntityExceptionEventArgs(e));
        }

        #endregion

        #region Events

        public event EntityExceptionEventHandler EntityException = delegate { };

        #endregion

    }
}
