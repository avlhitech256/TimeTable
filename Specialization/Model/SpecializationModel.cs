using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using System.Linq;
using Specialization.SearchCriteria;
using DataService.Constant;
using DataService.Entity.Specialization;
using DataService.Model;
using Domain.DomainContext;
using Domain.Model;

namespace Specialization.Model
{
    public class SpecializationModel : Model<DataService.Model.Specialization>, ISpecializationModel
    {
        private ObservableCollection<Specialty> specialties;
        private ObservableCollection<Specialty> specialtiesForSearch;
        private bool specialtiesIsLoaded;
        private bool specialtiesForSearchIsLoaded;

        public SpecializationModel(IDomainContext domainContext) : base(domainContext, new SpecializationSearchCriteria())
        {
            specialtiesIsLoaded = false;
            specialtiesForSearchIsLoaded = false;
        }

        #region Properties

        public ObservableCollection<Specialty> Specialties
        {
            get
            {
                if (!specialtiesIsLoaded)
                {
                    specialtiesIsLoaded = true;
                    CreateSpecialties();
                }

                return specialties;
            }

            private set
            {
                if (specialties != value)
                {
                    specialties = value;
                    OnPropertyChanged();
                }

            }

        }

        public ObservableCollection<Specialty> SpecialtiesForSearch
        {
            get
            {
                if (!specialtiesForSearchIsLoaded)
                {
                    specialtiesForSearchIsLoaded = true;
                    CreateSpecialtiesForSearch();
                }

                return specialtiesForSearch;
            }

            private set
            {
                if (specialtiesForSearch != value)
                {
                    specialtiesForSearch = value;
                    OnPropertyChanged();
                }

            }

        }

        public ObservableCollection<DataService.Model.Chair> Chairs => ((ISpecializationEntity)SelectedItem).Chairs;

        #endregion

        #region Methods

        protected override void OnSelectedItemChanged()
        {
            OnPropertyChanged(nameof(Chairs));
        }
        private void CreateSpecialties()
        {
            Specialties = new ObservableCollection<Specialty>();
            RefreshSpecialtyProperty();
        }

        private void RefreshSpecialtyProperty()
        {
            Specialties.Clear();

            try
            {
                DbContext.Specialties.OrderBy(x => x.Name).ToList().ForEach(x => Specialties.Add(x));
                OnPropertyChanged(nameof(Specialties));
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

        private void CreateSpecialtiesForSearch()
        {
            SpecialtiesForSearch = new ObservableCollection<Specialty>();
            RefreshSpecialtyForSearchProperty();
        }

        private void RefreshSpecialtyForSearchProperty()
        {
            SpecialtiesForSearch.Clear();

            try
            {
                Specialty item0 = new Specialty { Id = 0, Name = DafaultConstant.DefaultSpecialty };
                SpecialtiesForSearch.Add(item0);
                Specialties.ToList().ForEach(x => SpecialtiesForSearch.Add(x));
                OnPropertyChanged(nameof(SpecialtiesForSearch));
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

        public void RefreshSpecialties()
        {
            RefreshSpecialtyProperty();
            RefreshSpecialtyForSearchProperty();
        }

        public void RefreshChairs()
        {
            SelectedItem.RefreshChildItems();
        }

        protected override List<DataService.Model.Specialization> SelectEntities()
        {
            List<DataService.Model.Specialization> result = base.SelectEntities();
            SpecializationSearchCriteria searchCriteria = SearchCriteria as SpecializationSearchCriteria;

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
                    .Where(x => searchCriteria.SpecialtyId <= 0L || x.SpecialtyId == searchCriteria.SpecialtyId).ToList();
            }

            return result;
        }

        #endregion

    }
}