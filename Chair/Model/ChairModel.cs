using System.Collections.ObjectModel;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using System.Linq;
using Chair.SearchCriteria;
using DataService.Constant;
using DataService.Model;
using Domain.DomainContext;
using Domain.Model;

namespace Chair.Model
{
    public class ChairModel :  Model<DataService.Model.Chair>, IChairModel
    {
        private ObservableCollection<Faculty> faculties;
        private ObservableCollection<Faculty> facultiesForSearch;
        private ObservableCollection<Specialization> specializations;
        private bool facultiesIsLoaded;
        private bool facultiesForSearchIsLoaded;
        private bool specializationsIsLoaded;

        public ChairModel(IDomainContext domainContext) : base(domainContext, new ChairSearchCriteria())
        {
            facultiesIsLoaded = false;
            facultiesForSearchIsLoaded = false;
            specializationsIsLoaded = false;
        }

        #region Properties

        public ObservableCollection<Faculty> Faculties
        {
            get
            {
                if (!facultiesIsLoaded)
                {
                    facultiesIsLoaded = true;
                    CreateFaculties();
                }

                return faculties;
            }

            private set
            {
                if (faculties != value)
                {
                    faculties = value;
                    OnPropertyChanged();
                }

            }

        }

        public ObservableCollection<Faculty> FacultiesForSearch
        {
            get
            {
                if (!facultiesForSearchIsLoaded)
                {
                    facultiesForSearchIsLoaded = true;
                    CreateFacultiesForSearch();
                }

                return facultiesForSearch;
            }

            private set
            {
                if (facultiesForSearch != value)
                {
                    facultiesForSearch = value;
                    OnPropertyChanged();
                }

            }

        }

        public ObservableCollection<Specialization> Specializations
        {
            get { return specializations; }
        }

        #endregion

        #region Methods
        private void CreateFaculties()
        {
            Faculties = new ObservableCollection<Faculty>();
            RefreshFacultyProperty();
        }

        private void RefreshFacultyProperty()
        {
            Faculties.Clear();

            try
            {
                DbContext.Faculties.OrderBy(x => x.Name).ToList().ForEach(x => Faculties.Add(x));
                OnPropertyChanged(nameof(Faculties));
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

        private void CreateFacultiesForSearch()
        {
            FacultiesForSearch = new ObservableCollection<Faculty>();
            RefreshFacultyForSearchProperty();
        }

        private void RefreshFacultyForSearchProperty()
        {
            FacultiesForSearch.Clear();

            try
            {
                Faculty item0 = new Faculty { Id = 0, Name = DafaultConstant.DefaultFaculty };
                FacultiesForSearch.Add(item0);
                Faculties.ToList().ForEach(x => FacultiesForSearch.Add(x));
                OnPropertyChanged(nameof(FacultiesForSearch));
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

        public void RefreshFaculties()
        {
            RefreshFacultyProperty();
            RefreshFacultyForSearchProperty();
        }

        public void RefreshSpecializations()
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }

}
