using System.Windows.Controls;
using Common.Entry;

namespace HighSchool.View
{
    /// <summary>
    /// Логика взаимодействия для HighSchoolSearchControl.xaml
    /// </summary>
    public partial class HighSchoolSearchControl : EntryControl
    {
        #region Members

        private static HighSchoolSearchControl control;

        #endregion
        private HighSchoolSearchControl()
        {
            InitializeComponent();
        }

        public static HighSchoolSearchControl Instance()
        {
            return control ?? (control = new HighSchoolSearchControl());
        }
    }
}
