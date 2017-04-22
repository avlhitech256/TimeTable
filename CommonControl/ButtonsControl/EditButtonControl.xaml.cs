using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;
using Common.Annotations;

namespace CommonControl.ButtonsControl
{
    /// <summary>
    /// Логика взаимодействия для EditButtonControl.xaml
    /// </summary>
    public partial class EditButtonControl : UserControl, INotifyPropertyChanged
    {
        #region Constructors

        public EditButtonControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

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

        public object ToolTipSaveButton
        {
            get
            {
                return SaveButton.ToolTip;
            }

            set
            {
                if (SaveButton.ToolTip != value)
                {
                    SaveButton.ToolTip = value;
                    OnPropertyChanged();
                }

            }

        }

        public object ContentSaveButton
        {
            get
            {
                return SaveButton.Content;
            }

            set
            {
                if (SaveButton.Content != value)
                {
                    SaveButton.Content = value;
                    OnPropertyChanged();
                }

            }

        }

        public object ToolTipSaveAndNewButton
        {
            get
            {
                return SaveAndNewButton.ToolTip;
            }

            set
            {
                if (SaveAndNewButton.ToolTip != value)
                {
                    SaveAndNewButton.ToolTip = value;
                    OnPropertyChanged();
                }

            }

        }

        public object ContentSaveAndNewButton
        {
            get
            {
                return SaveAndNewButtonTextBlock.Text;
            }

            set
            {
                if (SaveAndNewButtonTextBlock.Text != value.ToString())
                {
                    SaveAndNewButtonTextBlock.Text = value.ToString();
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

        public object ToolTipBackToSearchButton
        {
            get
            {
                return BackToSearchButton.ToolTip;
            }

            set
            {
                if (BackToSearchButton.ToolTip != value)
                {
                    BackToSearchButton.ToolTip = value;
                    OnPropertyChanged();
                }

            }

        }

        public object ContentBackToSearchButton
        {
            get
            {
                return BackToSearchButton.Content;
            }

            set
            {
                if (BackToSearchButton.Content != value)
                {
                    BackToSearchButton.Content = value;
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
