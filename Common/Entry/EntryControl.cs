using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Common.Entry
{
    public class EntryControl : UserControl
    {
        #region Members

        private bool isModified;
        private bool enabledToCreate;
        private bool enabledToEdit;
        private bool enabledToSearch;
        private ControlState controlState;
        private static EntryControl entryControl;

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

        public DomainContext.DomainContext DomainContext { get; set; }

        #endregion

        #region Methods

        public static EntryControl Instance()
        {
            return entryControl ?? (entryControl = new EntryControl());
        }
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
