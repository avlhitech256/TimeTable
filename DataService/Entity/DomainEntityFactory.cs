using System;
using System.Collections.Generic;
using Common.Messenger;
using DataService.DataService;
using DataService.Entity.Chair;
using DataService.Entity.Employee;
using DataService.Entity.Faculty;
using DataService.Entity.HighSchool;
using DataService.Entity.Specialization;
using DataService.Entity.Specialty;

namespace DataService.Entity
{
    public class DomainEntityFactory : IDomainEntityFactory
    {
        #region Members

        private IDataService dataService;
        private IMessenger messenger;
        private readonly Dictionary<Type, Func<object, long, object>> mapEntity;
        private readonly Dictionary<Type, Func<object>> mapCreatorEntity;

        #endregion

        #region Comstructors

        public DomainEntityFactory(IDataService dataService, IMessenger messenger)
        {
            this.dataService = dataService;
            this.messenger = messenger;
            mapEntity = new Dictionary<Type, Func<object, long, object>>
            {
                {typeof(Model.HighSchool), (entity, position) => new HighSchoolEntity(dataService, messenger, entity as Model.HighSchool, position)},
                {typeof(Model.Faculty), (entity, position) => new FacultyEntity(dataService, messenger, entity as Model.Faculty, position)},
                {typeof(Model.Chair), (entity, position) => new ChairEntity(dataService, messenger, entity as Model.Chair, position)},
                {typeof(Model.Specialty), (entity, position) => new SpecialtyEntity(dataService, messenger, entity as Model.Specialty, position)},
                {typeof(Model.Specialization), (entity, position) => new SpecializationEntity(dataService, messenger, entity as Model.Specialization, position)},
                {typeof(Model.Employee), (entity, position) => new EmployeeEntity(dataService, messenger, entity as Model.Employee, position)}
            };

            mapCreatorEntity = new Dictionary<Type, Func<object>>
            {
                {typeof(Model.HighSchool), () => new HighSchoolEntity(dataService, messenger)},
                {typeof(Model.Faculty), () => new FacultyEntity(dataService, messenger)},
                {typeof(Model.Chair), () => new ChairEntity(dataService, messenger)},
                {typeof(Model.Specialty), () => new SpecialtyEntity(dataService, messenger)},
                {typeof(Model.Specialization), () => new SpecializationEntity(dataService, messenger)},
                {typeof(Model.Employee), () => new EmployeeEntity(dataService, messenger)}
            };
        }

        #endregion

        #region Methods

        public IDomainEntity<T> GetDomainEntity<T>() where T : class
        {
            IDomainEntity<T> result = null;

            if (mapCreatorEntity.ContainsKey(typeof(T)))
            {
                result = mapCreatorEntity[typeof(T)]() as IDomainEntity<T>;
            }

            return result;
        }

        public IDomainEntity<T> GetDomainEntity<T>(T entity, long position) where T : class
        {
            IDomainEntity<T> result = null;

            if (mapEntity.ContainsKey(typeof (T)))
            {
                result = mapEntity[typeof (T)](entity, position) as IDomainEntity<T>;
            } 

            return result;
        }

        #endregion
    }
}
