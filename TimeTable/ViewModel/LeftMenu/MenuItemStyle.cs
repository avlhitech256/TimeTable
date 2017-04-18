using Common.Data.Notifier;
using Domain.Data.Enum;

namespace TimeTable.ViewModel.LeftMenu
{
    public class MenuItemStyle : Notifier
    {
        #region Members

        private MenuItemName name;
        private string backgroundColorString;
        private string notSelectedAndMouseIsNotOverColorString;
        private string notSelectedAndMouseIsOverColorString;
        private string selectedAndMouseIsNotOverColorString;
        private string selectedAndMouseIsOverColorString;

        private bool selected;
        private bool isMouseOver;

        #endregion

        #region Constructors

        public MenuItemStyle() : this(MenuItemName.HighSchool, null, null, null, null)
        {
        }

        public MenuItemStyle(MenuItemName name,
                             string notSelectedAndMouseIsNotOverColorString,
                             string notSelectedAndMouseIsOverColorString,
                             string selectedAndMouseIsNotOverColorString,
                             string selectedAndMouseIsOverColorString)
        {
            this.name = name;
            this.notSelectedAndMouseIsNotOverColorString = notSelectedAndMouseIsNotOverColorString;
            this.notSelectedAndMouseIsOverColorString = notSelectedAndMouseIsOverColorString;
            this.selectedAndMouseIsNotOverColorString = selectedAndMouseIsNotOverColorString;
            this.selectedAndMouseIsOverColorString = selectedAndMouseIsOverColorString;
            SetDefaultBackgroundColors();

            backgroundColorString = notSelectedAndMouseIsNotOverColorString;
            selected = false;
            isMouseOver = false;
        }

        #endregion

        #region Properties

        public MenuItemName Name
        {
            get
            {
                return name;
            }

            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged();
                }

            }

        }

        public string NotSelectedAndMouseIsNotOverBackgroundColor
        {
            get
            {
                return notSelectedAndMouseIsNotOverColorString;
            }

            set
            {
                if (notSelectedAndMouseIsNotOverColorString != value)
                {
                    notSelectedAndMouseIsNotOverColorString = value;
                    OnPropertyChanged();
                    ChangeBackgroundBrush();
                }
            }
        }

        public string NotSelectedAndMouseIsOverBackgroundColor
        {
            get
            {
                return notSelectedAndMouseIsOverColorString;
            }

            set
            {
                if (notSelectedAndMouseIsOverColorString != value)
                {
                    notSelectedAndMouseIsOverColorString = value;
                    OnPropertyChanged();
                    ChangeBackgroundBrush();
                }

            }

        }

        public string SelectedAndMouseIsNotOverBackgroundColor
        {
            get
            {
                return selectedAndMouseIsNotOverColorString;
            }

            set
            {
                if (selectedAndMouseIsNotOverColorString != value)
                {
                    selectedAndMouseIsNotOverColorString = value;
                    OnPropertyChanged();
                    ChangeBackgroundBrush();
                }

            }

        }

        public string SelectedAndMouseIsOverBackgroundColor
        {
            get
            {
                return selectedAndMouseIsOverColorString;
            }

            set
            {
                if (selectedAndMouseIsOverColorString != value)
                {
                    selectedAndMouseIsOverColorString = value;
                    OnPropertyChanged();
                    ChangeBackgroundBrush();
                }

            }

        }

        public bool IsMouseOver
        {
            get
            {
                return isMouseOver;
            }

            set
            {
                if (isMouseOver != value)
                {
                    isMouseOver = value;
                    OnPropertyChanged();
                    ChangeBackgroundBrush();
                }

            }

        }

        public bool Selected
        {
            get
            {
                return selected;
            }

            set
            {
                if (selected != value)
                {
                    selected = value;
                    OnPropertyChanged();
                    ChangeBackgroundBrush();
                }

            }

        }

        public string Background
        {
            get
            {
                return backgroundColorString;
            }

            set
            {
                if (backgroundColorString != value)
                {
                    backgroundColorString = value;
                    OnPropertyChanged();
                }

            }

        }

        #endregion

        #region Methods

        private void SetDefaultBackgroundColors()
        {
            if (NotSelectedAndMouseIsNotOverBackgroundColor == null)
            {
                NotSelectedAndMouseIsNotOverBackgroundColor = "#FF808080";
            }

            if (NotSelectedAndMouseIsOverBackgroundColor == null)
            {
                NotSelectedAndMouseIsOverBackgroundColor = "#FF646464";
            }

            if (SelectedAndMouseIsNotOverBackgroundColor == null)
            {
                SelectedAndMouseIsNotOverBackgroundColor = "#FF4747B8";
            }

            if (SelectedAndMouseIsOverBackgroundColor == null)
            {
                SelectedAndMouseIsOverBackgroundColor = "#FF6767D8";
            }

        }

        private void ChangeBackgroundBrush()
        {
            Background = Selected
                ? (IsMouseOver ? SelectedAndMouseIsOverBackgroundColor : SelectedAndMouseIsNotOverBackgroundColor)
                : (IsMouseOver ? NotSelectedAndMouseIsOverBackgroundColor : NotSelectedAndMouseIsNotOverBackgroundColor);
        }

        public bool Equals(MenuItemStyle other)
        {
            bool result = Selected == other.Selected &&
                          IsMouseOver == other.IsMouseOver &&
                          Background == other.Background &&
                          NotSelectedAndMouseIsNotOverBackgroundColor == other.NotSelectedAndMouseIsNotOverBackgroundColor &&
                          NotSelectedAndMouseIsOverBackgroundColor == other.NotSelectedAndMouseIsOverBackgroundColor &&
                          SelectedAndMouseIsNotOverBackgroundColor == other.SelectedAndMouseIsNotOverBackgroundColor &&
                          SelectedAndMouseIsOverBackgroundColor == other.SelectedAndMouseIsOverBackgroundColor;

            return result;
        }

        #endregion
    }

}
