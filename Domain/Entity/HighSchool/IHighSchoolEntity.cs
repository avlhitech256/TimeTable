using System;
using System.ComponentModel;
using DataService.Model;

namespace Domain.Entity.HighSchool
{
    public interface IHighSchoolEntity : IDomainEntity<DataService.Model.HighSchool>
    {
        long Rector { get; set; }
        Employee Employee { get; set; }
    }
}
