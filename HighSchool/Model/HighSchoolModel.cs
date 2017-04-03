using System.Collections.ObjectModel;
using System.Linq;
using DataService.DataService;

namespace HighSchool.Model
{
    public class HighSchoolModel : IHighSchoolModel
    {
        #region Members

        private readonly IDataService dataService;

        #endregion

        #region Constructors

        public HighSchoolModel(IDataService dataService)
        {
            this.dataService = dataService;
            InitializeHighSchools();
        }

        #endregion

        #region Properties

        public ObservableCollection<DataService.Model.HighSchool> HighSchools { get; set; }

        #endregion

        #region Methods

        private void InitializeHighSchools()
        {
            HighSchools = new ObservableCollection<DataService.Model.HighSchool>();
            dataService.GetHighSchools().ForEach(x => HighSchools.Add(x));
        }

        private DataService.Model.HighSchool GetHighSchool(long id)
        {
            DataService.Model.HighSchool highSchool =
                HighSchools.FirstOrDefault(x => x.Id == id);
            return highSchool;
        }

        public void UpdateHighSchool()
        {
            
        }

        #endregion

    }

}
