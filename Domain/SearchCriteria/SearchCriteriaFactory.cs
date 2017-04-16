using System;
using System.Collections.Generic;
using Domain.SearchCriteria.HighSchool;

namespace Domain.SearchCriteria
{
    public class SearchCriteriaFactory
    {
        #region Members

        private readonly Dictionary<Type, Func<ISearchCriteria>> mapCriteria;

        #endregion

        #region Constructors

        public SearchCriteriaFactory()
        {
            mapCriteria = new Dictionary<Type, Func<ISearchCriteria>>
            {
                {typeof(DataService.Model.HighSchool), () => new HighSchoolSearchCriteria()}
            };
        }

        #endregion

        #region Methods

        public ISearchCriteria GetSearchCriteria(Type type)
        {
            ISearchCriteria result = mapCriteria.ContainsKey(type) ? mapCriteria[type]() : new SearchCriteria();

            return result;
        }

        #endregion
    }
}
