using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using System.Linq;
using Chair.SearchCriteria;
using DataService.Constant;
using DataService.Entity.Chair;
using DataService.Model;
using Domain.DomainContext;
using Domain.Model;

namespace Chair.Model
{
    public class ChairModel : Model<DataService.Model.Chair>, IChairModel
    {
        private ObservableCollection<Faculty> faculties;
        private ObservableCollection<Faculty> facultiesForSearch;
        private bool facultiesIsLoaded;
        private bool facultiesForSearchIsLoaded;

        public ChairModel(IDomainContext domainContext) : base(domainContext, new ChairSearchCriteria())
        {
            facultiesIsLoaded = false;
            facultiesForSearchIsLoaded = false;
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

        public ObservableCollection<Specialization> Specializations => ((IChairEntity) SelectedItem).Specializations;

        #endregion

        #region Methods

        protected override void OnSelectedItemChanged()
        {
            OnPropertyChanged(nameof(Specializations));
        }
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
                Faculty item0 = new Faculty { Id = 0, Name = string.Empty };
                Faculties.Add(item0);
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
                DbContext.Faculties.OrderBy(x => x.Name).ToList().ForEach(x => FacultiesForSearch.Add(x));
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
            SelectedItem.RefreshChildItems();
        }

        protected override List<DataService.Model.Chair> SelectEntities()
        {
            List<DataService.Model.Chair> result = base.SelectEntities();
            ChairSearchCriteria searchCriteria = SearchCriteria as ChairSearchCriteria;

            if (searchCriteria != null)
            {
                result = base.SelectEntities()
                    .Where(x => string.IsNullOrWhiteSpace(searchCriteria.Code) ||
                                x.Code.ToUpperInvariant()
                                    .Contains(searchCriteria.Code.ToUpperInvariant())).ToList()
                    .Where(x => string.IsNullOrWhiteSpace(searchCriteria.Name) ||
                                x.Name.ToUpperInvariant()
                                    .Contains(searchCriteria.Name.ToUpperInvariant())).ToList()
                    .Where(x => !searchCriteria.Active || x.Active).ToList()
                    .Where(x => (!searchCriteria.CreatedFrom.HasValue ||
                                 x.Created >= searchCriteria.CreatedFrom.Value) &&
                                (!searchCriteria.CreatedTo.HasValue ||
                                 x.Created < searchCriteria.CreatedTo.Value.AddDays(1))).ToList()
                    .Where(x => (!searchCriteria.LastModifyFrom.HasValue ||
                                 x.LastModify >= searchCriteria.LastModifyFrom.Value) &&
                                (!searchCriteria.LastModifyTo.HasValue ||
                                 x.LastModify < searchCriteria.LastModifyTo.Value.AddDays(1))).ToList()
                    .Where(x => string.IsNullOrWhiteSpace(searchCriteria.UserModify) ||
                                x.UserModify.ToUpperInvariant()
                                    .Contains(searchCriteria.UserModify.ToUpperInvariant())).ToList()
                    .Where(x => searchCriteria.FacultyId <= 0L || x.FacultyId == searchCriteria.FacultyId).ToList();
            }

            return result;
        }

        #endregion

    }

}
