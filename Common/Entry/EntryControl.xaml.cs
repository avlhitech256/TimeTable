using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Common.Entry
{
    /// <summary>
    /// Логика взаимодействия для EntryControl.xaml
    /// </summary>
    public partial class EntryControl : UserControl
    {
        #region Members

        private bool isModified;
        private bool enabledToCreate;
        private bool enabledToEdit;
        private bool enabledToSearch;
        private ControlState controlState;

        #endregion

        #region Constructors

        public EntryControl()
        {
            AccessToCreate = false;
            AccessToEdit = false;
            AccessToSearch = false;
            ControlState = ControlState.ReadOnly;
            IsModified = false;
            EnabledToCreate = false;
            EnabledToEdit = false;
            EnabledToSearch = false;

            InitializeComponent();
        }

        #endregion

        #region Properties

        public bool AccessToCreate { get; set; }

        public bool AccessToEdit { get; set; }

        public bool AccessToSearch { get; set; }

        public bool IsModified
        {
            get
            {
                return isModified;
            }

            private set
            {
                if (value &&
                    (ControlState == ControlState.New && AccessToCreate ||
                     ControlState == ControlState.Edit && AccessToEdit))
                {
                    isModified = true;
                }
                else
                {
                    isModified = false;
                }

            }

        }

        public bool EnabledToCreate
        {
            get
            {
                return enabledToCreate;
            }

            private set
            {
                if (value && ControlState == ControlState.New && AccessToCreate)
                {
                    enabledToCreate = true;
                }
                else
                {
                    enabledToCreate = false;
                }

            }

        }

        public bool EnabledToEdit
        {
            get
            {
                return enabledToEdit;
            }

            private set
            {
                if (value && ControlState == ControlState.Edit && AccessToEdit)
                {
                    enabledToEdit = true;
                }
                else
                {
                    enabledToEdit = false;
                }

            }

        }

        public bool EnabledToSearch
        {
            get
            {
                return enabledToSearch;
            }

            private set
            {
                if (value && ControlState == ControlState.ReadOnly && AccessToSearch)
                {
                    enabledToSearch = true;
                }
                else
                {
                    enabledToSearch = false;
                }

            }

        }

        public ControlState ControlState { get; private set; }

        #endregion

        #region Methods

        public virtual bool New()
        {
            bool result = true;

            ControlState = ControlState.New;
            IsModified = false;
            EnabledToCreate = false;
            EnabledToEdit = false;
            EnabledToSearch = false;

            return result;
        }

        public virtual bool Edit()
        {
            bool result = true;

            ControlState = ControlState.Edit;
            IsModified = false;
            EnabledToCreate = false;
            EnabledToEdit = false;
            EnabledToSearch = false;

            return result;
        }

        public virtual bool Save()
        {
            bool result = true;

            if (IsModified)
            {
                string message = "Вы хотите записать изменения?";
                string caption = "Сохранение";
                MessageBoxButton buttons = MessageBoxButton.YesNoCancel;
                MessageBoxImage icon = MessageBoxImage.Question;
                MessageBoxResult defaultResult = MessageBoxResult.Yes;
                MessageBoxOptions options = MessageBoxOptions.DefaultDesktopOnly;
                if (MessageBox.Show(message, caption, buttons, icon, defaultResult, options) == MessageBoxResult.Cancel)
                {
                    result = false;
                }
            }

            if (result)
            {
                IsModified = false;
                EnabledToCreate = true;
                EnabledToEdit = true;
                EnabledToSearch = true;
            }

            return result;
        }

        public virtual bool Load()
        {
            bool result = true;
            IsModified = false;

            if (ControlState != ControlState.ReadOnly)
            {
                EnabledToCreate = false;
                EnabledToEdit = false;
                EnabledToSearch = false;
            }
            else
            {
                EnabledToCreate = true;
                EnabledToEdit = true;
                EnabledToSearch = true;
            }

            return result;
        }

        public virtual bool Refresh()
        {
            bool result = true;
            IsModified = false;

            return result;
        }

        #endregion
    }
}
