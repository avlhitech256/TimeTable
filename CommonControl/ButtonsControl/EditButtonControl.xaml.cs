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
        #region Members

        private Brush backgroundAddButton;
        private Brush backgroundEditButton;
        private Brush backgroundSaveButton;
        private Brush backgroundSaveAndNewButton;
        private Brush backgroundDeleteButton;
        private Brush backgroundBackToSearchButton;

        #endregion

        #region Constructors

        public EditButtonControl()
        {
            InitializeComponent();
            InitializeProperties();
            SubscribeEventsButton();
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

        public Brush BackgroundAddButton
        {
            get
            {
                return AddButton.Background;
            }

            set
            {
                if (!AddButton.Background.Equals(value))
                {
                    AddButton.Background = value;
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

        public Brush BackgroundEditButton
        {
            get
            {
                return EditButton.Background;
            }

            set
            {
                if (!EditButton.Background.Equals(value))
                {
                    EditButton.Background = value;
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

        public Brush BackgroundSaveButton
        {
            get
            {
                return SaveButton.Background;
            }

            set
            {
                if (!SaveButton.Background.Equals(value))
                {
                    SaveButton.Background = value;
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

        public Brush BackgroundSaveAndNewButton
        {
            get
            {
                return SaveAndNewButton.Background;
            }

            set
            {
                if (!SaveAndNewButton.Background.Equals(value))
                {
                    SaveAndNewButton.Background = value;
                    SaveAndNewButtonTextBlock.Background = value;
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

        public Brush BackgroundDeleteButton
        {
            get
            {
                return DeleteButton.Background;
            }

            set
            {
                if (!DeleteButton.Background.Equals(value))
                {
                    DeleteButton.Background = value;
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

        public Brush BackgroundBackToSearchButton
        {
            get
            {
                return BackToSearchButton.Background;
            }

            set
            {
                if (!BackToSearchButton.Background.Equals(value))
                {
                    BackToSearchButton.Background = value;
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

        private void InitializeProperties()
        {
            backgroundAddButton = BackgroundAddButton;
            backgroundEditButton = BackgroundEditButton;
            backgroundSaveButton = BackgroundSaveButton;
            backgroundSaveAndNewButton = BackgroundSaveAndNewButton;
            backgroundDeleteButton = BackgroundDeleteButton;
            backgroundBackToSearchButton = BackgroundBackToSearchButton;
            SaveAndNewButtonTextBlock.Background = BackgroundSaveAndNewButton;
        }

        public void SubscribeEventsButton()
        {
            if (AddButton?.Command != null)
            {
                AddButton.Command.CanExecuteChanged += ChangeBackgroundAddButton;
            }

            if (EditButton?.Command != null)
            {
                EditButton.Command.CanExecuteChanged += ChangeBackgroundEditButton;
            }

            if (SaveButton?.Command != null)
            {
                SaveButton.Command.CanExecuteChanged += ChangeBackgroundSaveButton;
            }

            if (SaveAndNewButton?.Command != null)
            {
                SaveAndNewButton.Command.CanExecuteChanged += ChangeBackgroundSaveAndNewButton;
            }

            if (DeleteButton?.Command != null)
            {
                DeleteButton.Command.CanExecuteChanged += ChangeBackgroundDeleteButton;
            }

            if (BackToSearchButton?.Command != null)
            {
                BackToSearchButton.Command.CanExecuteChanged += ChangeBackgroundBackToSearchButton;
            }

        }

        private void ChangeBackgroundAddButton(object sender, EventArgs e)
        {
            if (!BackgroundAddButton.Equals(backgroundAddButton))
            {
                backgroundAddButton = BackgroundAddButton;
                OnPropertyChanged(nameof(BackgroundAddButton));
            }
        }

        private void ChangeBackgroundEditButton(object sender, EventArgs e)
        {
            if (!BackgroundEditButton.Equals(backgroundAddButton))
            {
                backgroundEditButton = BackgroundEditButton;
                OnPropertyChanged(nameof(BackgroundEditButton));
            }
        }

        private void ChangeBackgroundSaveButton(object sender, EventArgs e)
        {
            if (!BackgroundSaveButton.Equals(backgroundSaveButton))
            {
                backgroundSaveButton = BackgroundSaveButton;
                OnPropertyChanged(nameof(BackgroundSaveButton));
            }
        }

        private void ChangeBackgroundSaveAndNewButton(object sender, EventArgs e)
        {
            if (!BackgroundSaveAndNewButton.Equals(backgroundSaveAndNewButton))
            {
                backgroundSaveAndNewButton = BackgroundSaveAndNewButton;
                SaveAndNewButtonTextBlock.Background = BackgroundSaveAndNewButton;
                OnPropertyChanged(nameof(BackgroundSaveAndNewButton));
            }
        }

        private void ChangeBackgroundDeleteButton(object sender, EventArgs e)
        {
            if (!BackgroundDeleteButton.Equals(backgroundDeleteButton))
            {
                backgroundDeleteButton = BackgroundDeleteButton;
                OnPropertyChanged(nameof(BackgroundDeleteButton));
            }
        }

        private void ChangeBackgroundBackToSearchButton(object sender, EventArgs e)
        {
            if (!BackgroundBackToSearchButton.Equals(backgroundBackToSearchButton))
            {
                backgroundBackToSearchButton = BackgroundBackToSearchButton;
                OnPropertyChanged(nameof(BackgroundBackToSearchButton));
            }
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
