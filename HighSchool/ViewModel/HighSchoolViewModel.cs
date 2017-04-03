﻿using System;
using Common.Data.Criteria;
using Common.Data.Notifier;
using Domain.DomainContext;

namespace HighSchool.ViewModel
{
    public class HighSchoolViewModel : Notifier
    {
        #region Members

        private DataService.Model.HighSchool highSchool;
        private IDomainContext context;

        #endregion

        #region Constructors
        public HighSchoolViewModel(IDomainContext context)
        {
            this.context = context;
            SearchCriteria = new SearchCriteria();
        }

        #endregion

        #region Properties

        public SearchCriteria SearchCriteria { get; }

        public string Code
        {
            get
            {
                return highSchool.Code;
            }

            set
            {
                if (highSchool.Code != value)
                {
                    highSchool.Code = value;
                    OnPropertyChanged();
                }
            }

        }

        public string Name
        {
            get
            {
                return highSchool.Name;
            }

            set
            {
                if (highSchool.Name != value)
                {
                    highSchool.Name = value;
                    OnPropertyChanged();
                }

            }
        }

        public bool Active
        {
            get
            {
                return highSchool.Active;
            }

            set
            {
                if (highSchool.Active != value)
                {
                    highSchool.Active = value;
                    OnPropertyChanged();
                }

            }

        }

        public DateTimeOffset Cteated
        {
            get
            {
                return highSchool.Cteated;
            }

            set
            {
                if (highSchool.Cteated != value)
                {
                    highSchool.Cteated = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTimeOffset LastModify
        {
            get
            {
                return highSchool.LastModify;
            }

            set
            {
                if (highSchool.LastModify != value)
                {
                    highSchool.LastModify = value;
                    OnPropertyChanged();
                }
            }
        }

        public string UserModify
        {
            get
            {
                return highSchool.UserModify;
            }

            set
            {
                if (highSchool.UserModify != value)
                {
                    highSchool.UserModify = value;
                    OnPropertyChanged();
                }

            }
        }


        #endregion

        #region Methods

        private void SetHighSchool(DataService.Model.HighSchool highSchoolItem)
        {
            highSchool = highSchoolItem;
            OnPropertyChanged(nameof(Code));
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Active));
            OnPropertyChanged(nameof(Cteated));
            OnPropertyChanged(nameof(LastModify));
            OnPropertyChanged(nameof(UserModify));
        }

        #endregion

    }
}
