using System;
using System.Windows.Controls;

namespace HighSchool.View
{
    /// <summary>
    /// Логика взаимодействия для HighSchoolSearchFieldsControl.xaml
    /// </summary>
    public partial class HighSchoolSearchFieldsControl : UserControl
    {
        public HighSchoolSearchFieldsControl()
        {
            InitializeComponent();
            SetDefaultSearchCriteria();
        }

        private void SetDefaultSearchCriteria()
        {
            DateTime now = DateTime.Now;
            HighSchoolSearchDateFrom.SelectedDate = now.Date.AddYears(-1);
            HighSchoolSearchDateTo.SelectedDate = now.Date;
            HighSchoolSearchActive.IsChecked = true;
        }

    }

}
