﻿using System.Collections.ObjectModel;
using DataService.Model;
using Domain.Model;

namespace Faculty.Model
{
    public interface IFacultyModel : IModel<DataService.Model.Faculty>
    {
        ObservableCollection<HighSchool> HighSchools { get; }
        ObservableCollection<HighSchool> HighSchoolsForSearch { get; }
        ObservableCollection<Chair> Chairs { get; }
        void RefreshHighSchools();
        void RefreshChairs();
    }
}
