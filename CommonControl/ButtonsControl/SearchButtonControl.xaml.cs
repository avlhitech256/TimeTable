using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;
using Common.Annotations;

namespace CommonControl.ButtonsControl
{
    /// <summary>
    /// Логика взаимодействия для SearchButtonControl.xaml
    /// </summary>
    public partial class SearchButtonControl : UserControl, INotifyPropertyChanged
    {
        #region Constructors

        public SearchButtonControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        public object ToolTipSearchButton
        {
            get
            {
                return SearchButton.ToolTip;
            }

            set
            {
                if (SearchButton.ToolTip != value)
                {
                    SearchButton.ToolTip = value;
                    OnPropertyChanged();
                }

            }

        }

        public object ContentSearchButton
        {
            get
            {
                return SearchButton.Content;
            }

            set
            {
                if (SearchButton.Content != value)
                {
                    SearchButton.Content = value;
                    OnPropertyChanged();
                }

            }

        }

        public object ToolTipClearButton
        {
            get
            {
                return ClearButton.ToolTip;
            }

            set
            {
                if (ClearButton.ToolTip != value)
                {
                    ClearButton.ToolTip = value;
                    OnPropertyChanged();
                }

            }

        }

        public object ContentClearButton
        {
            get
            {
                return ClearButton.Content;
            }

            set
            {
                if (ClearButton.Content != value)
                {
                    ClearButton.Content = value;
                    OnPropertyChanged();
                }

            }

        }

        public object ToolTipAddButton
        {
            get
            {
                return AddButton.ToolTip;
            }

            set
            {
                if (AddButton.ToolTip != value)
                {
                    AddButton.ToolTip = value;
                    OnPropertyChanged();
                }

            }

        }

        public object ContentAddButton
        {
            get
            {
                return AddButton.Content;
            }

            set
            {
                if (AddButton.Content != value)
                {
                    AddButton.Content = value;
                    OnPropertyChanged();
                }

            }

        }

        public object ToolTipViewButton
        {
            get
            {
                return ViewButton.ToolTip;
            }

            set
            {
                if (ViewButton.ToolTip != value)
                {
                    ViewButton.ToolTip = value;
                    OnPropertyChanged();
                }

            }

        }

        public object ContentViewButton
        {
            get
            {
                return ViewButton.Content;
            }

            set
            {
                if (ViewButton.Content != value)
                {
                    ViewButton.Content = value;
                    OnPropertyChanged();
                }

            }

        }

        public object ToolTipEditButton
        {
            get
            {
                return EditButton.ToolTip;
            }

            set
            {
                if (EditButton.ToolTip != value)
                {
                    EditButton.ToolTip = value;
                    OnPropertyChanged();
                }

            }

        }

        public object ContentEditButton
        {
            get
            {
                return EditButton.Content;
            }

            set
            {
                if (EditButton.Content != value)
                {
                    EditButton.Content = value;
                    OnPropertyChanged();
                }

            }

        }

        public object ToolTipDeleteButton
        {
            get
            {
                return DeleteButton.ToolTip;
            }

            set
            {
                if (DeleteButton.ToolTip != value)
                {
                    DeleteButton.ToolTip = value;
                    OnPropertyChanged();
                }

            }

        }

        public object ContentDeleteButton
        {
            get
            {
                return DeleteButton.Content;
            }

            set
            {
                if (DeleteButton.Content != value)
                {
                    DeleteButton.Content = value;
                    OnPropertyChanged();
                }
            }

        }

        #endregion

        #region Methods
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
