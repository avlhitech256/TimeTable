using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Domain.Data.Enum;
using Domain.Data.Notifier;
using Domain.Event;

namespace TimeTable.ViewModel.LeftMenu
{
    public class MenuItemsStyle : Notifier
    {
        #region Members

        private string notSelectedAndMouseIsNotOverBackgroundColor;
        private string notSelectedAndMouseIsOverBackgroundColor;
        private string selectedAndMouseIsNotOverBackgroundColor;
        private string selectedAndMouseIsOverBackgroundColor;

        private List<MenuItemStyle> menuItems; 
        private MenuItemStyle highSchoolMenuItemStyle;
        private MenuItemStyle facultyMenuItemStyle;
        private MenuItemStyle chairMenuItemStyle;
        private MenuItemStyle specialtyMenuItemStyle;
        private MenuItemStyle specializationMenuItemStyle;

        #endregion

        #region Constructors

        public MenuItemsStyle() : this(null, null, null, null)
        {
        } 

        public MenuItemsStyle(string notSelectedAndMouseIsNotOverColorString,
                             string notSelectedAndMouseIsOverColorString,
                             string selectedAndMouseIsNotOverColorString,
                             string selectedAndMouseIsOverColorString)
        {
            notSelectedAndMouseIsNotOverBackgroundColor = notSelectedAndMouseIsNotOverColorString;
            notSelectedAndMouseIsOverBackgroundColor = notSelectedAndMouseIsOverColorString;
            selectedAndMouseIsNotOverBackgroundColor = selectedAndMouseIsNotOverColorString;
            selectedAndMouseIsOverBackgroundColor = selectedAndMouseIsOverColorString;

            highSchoolMenuItemStyle = new MenuItemStyle(MenuItemName.HighSchool,
                                                        notSelectedAndMouseIsNotOverColorString,
                                                        notSelectedAndMouseIsOverColorString,
                                                        selectedAndMouseIsNotOverColorString,
                                                        selectedAndMouseIsOverColorString);
            facultyMenuItemStyle = new MenuItemStyle(MenuItemName.Faculty,
                                                     notSelectedAndMouseIsNotOverColorString,
                                                     notSelectedAndMouseIsOverColorString,
                                                     selectedAndMouseIsNotOverColorString,
                                                     selectedAndMouseIsOverColorString);
            chairMenuItemStyle = new MenuItemStyle(MenuItemName.Chair,
                                                   notSelectedAndMouseIsNotOverColorString,
                                                   notSelectedAndMouseIsOverColorString,
                                                   selectedAndMouseIsNotOverColorString,
                                                   selectedAndMouseIsOverColorString);
            specialtyMenuItemStyle = new MenuItemStyle(MenuItemName.Specialty,
                                                       notSelectedAndMouseIsNotOverColorString,
                                                       notSelectedAndMouseIsOverColorString,
                                                       selectedAndMouseIsNotOverColorString,
                                                       selectedAndMouseIsOverColorString);
            specializationMenuItemStyle = new MenuItemStyle(MenuItemName.Specialization,
                                                            notSelectedAndMouseIsNotOverColorString,
                                                            notSelectedAndMouseIsOverColorString,
                                                            selectedAndMouseIsNotOverColorString,
                                                            selectedAndMouseIsOverColorString);

            menuItems = new List<MenuItemStyle>();

            AddMenuItem(highSchoolMenuItemStyle);
            AddMenuItem(facultyMenuItemStyle);
            AddMenuItem(chairMenuItemStyle);
            AddMenuItem(specialtyMenuItemStyle);
            AddMenuItem(specializationMenuItemStyle);
        }

        #endregion

        #region Properties

        public string NotSelectedAndMouseIsNotOverBackgroundColor
        {
            get
            {
                return notSelectedAndMouseIsNotOverBackgroundColor;
            }

            set
            {
                if (notSelectedAndMouseIsNotOverBackgroundColor == null || notSelectedAndMouseIsNotOverBackgroundColor.Equals(value))
                {
                    notSelectedAndMouseIsNotOverBackgroundColor = value;
                    OnPropertyChanged();
                    menuItems.ForEach(
                        item =>
                        {
                            item.NotSelectedAndMouseIsNotOverBackgroundColor = NotSelectedAndMouseIsNotOverBackgroundColor;
                        });
                }
            }
        }

        public string NotSelectedAndMouseIsOverBackgroundColor
        {
            get
            {
                return notSelectedAndMouseIsOverBackgroundColor;
            }

            set
            {
                if (notSelectedAndMouseIsOverBackgroundColor != value)
                {
                    notSelectedAndMouseIsOverBackgroundColor = value;
                    OnPropertyChanged();
                    menuItems.ForEach(
                        item =>
                        {
                            item.NotSelectedAndMouseIsOverBackgroundColor = NotSelectedAndMouseIsOverBackgroundColor;
                        });
                }

            }

        }

        public string SelectedAndMouseIsNotOverBackgroundColor
        {
            get
            {
                return selectedAndMouseIsNotOverBackgroundColor;
            }

            set
            {
                if (selectedAndMouseIsNotOverBackgroundColor == null || selectedAndMouseIsNotOverBackgroundColor.Equals(value))
                {
                    selectedAndMouseIsNotOverBackgroundColor = value;
                    OnPropertyChanged();
                    menuItems.ForEach(
                        item =>
                        {
                            item.SelectedAndMouseIsNotOverBackgroundColor = SelectedAndMouseIsNotOverBackgroundColor;
                        });
                }

            }

        }

        public string SelectedAndMouseIsOverBackgroundColor
        {
            get
            {
                return selectedAndMouseIsOverBackgroundColor;
            }

            set
            {
                if (selectedAndMouseIsOverBackgroundColor == null || selectedAndMouseIsOverBackgroundColor.Equals(value))
                {
                    selectedAndMouseIsOverBackgroundColor = value;
                    OnPropertyChanged();
                    menuItems.ForEach(
                        item =>
                        {
                            item.SelectedAndMouseIsOverBackgroundColor = SelectedAndMouseIsOverBackgroundColor;
                        });
                }

            }

        }

        public MenuItemStyle HighSchoolMenuItemStyle
        {
            get
            {
                return highSchoolMenuItemStyle;
            }

            set
            {
                if (highSchoolMenuItemStyle == null || !highSchoolMenuItemStyle.Equals(value))
                {
                    highSchoolMenuItemStyle = ReplaceMenuItem(highSchoolMenuItemStyle, value);
                    OnPropertyChanged();
                }

            }

        }

        public MenuItemStyle FacultyMenuItemStyle
        {
            get
            {
                return facultyMenuItemStyle;
            }

            set
            {
                if (facultyMenuItemStyle == null || !facultyMenuItemStyle.Equals(value))
                {
                    facultyMenuItemStyle = ReplaceMenuItem(facultyMenuItemStyle, value);
                    OnPropertyChanged();
                }

            }

        }

        public MenuItemStyle ChairMenuItemStyle
        {
            get
            {
                return chairMenuItemStyle;
            }

            set
            {
                if (chairMenuItemStyle == null || !chairMenuItemStyle.Equals(value))
                {
                    chairMenuItemStyle = ReplaceMenuItem(chairMenuItemStyle, value);
                    OnPropertyChanged();
                }

            }

        }

        public MenuItemStyle SpecialtyMenuItemStyle
        {
            get
            {
                return specialtyMenuItemStyle;
            }

            set
            {
                if (specialtyMenuItemStyle == null || !specialtyMenuItemStyle.Equals(value))
                {
                    specialtyMenuItemStyle = ReplaceMenuItem(specialtyMenuItemStyle, value);
                    OnPropertyChanged();
                }

            }

        }

        public MenuItemStyle SpecializationMenuItemStyle
        {
            get
            {
                return specializationMenuItemStyle;
            }

            set
            {
                if (specializationMenuItemStyle == null || !specializationMenuItemStyle.Equals(value))
                {
                    specializationMenuItemStyle = ReplaceMenuItem(specializationMenuItemStyle, value);
                    OnPropertyChanged();
                }

            }

        }

        #endregion
        
        #region Methods

        private MenuItemStyle ReplaceMenuItem(MenuItemStyle oldMenuItem, MenuItemStyle newMenuItem)
        {
            DeleteMenuItem(oldMenuItem);
            AddMenuItem(newMenuItem);

            return newMenuItem;
        }

        private void AddMenuItem(MenuItemStyle menuItem)
        {
            menuItems.Add(menuItem);
            SubscribeToEventOnChangeMenuItem(menuItem);
        }

        private void DeleteMenuItem(MenuItemStyle menuItem)
        {
            UnsubscribeToEventOnChangeMenuItem(menuItem);
            menuItems.Remove(menuItem);
        }

        private void SubscribeToEventOnChangeMenuItem(MenuItemStyle menuItem)
        {
            menuItem.PropertyChanged += RefreshMenuItems;
        }

        private void UnsubscribeToEventOnChangeMenuItem(MenuItemStyle menuItem)
        {
            menuItem.PropertyChanged -= RefreshMenuItems;
        }

        private void RefreshMenuItems(object sender, PropertyChangedEventArgs args)
        {
            if (args != null && args.PropertyName == "Selected")
            {
                MenuItemStyle menuItem = sender as MenuItemStyle;

                if (menuItem != null && menuItem.Selected)
                {
                    menuItems.Where(item => item != menuItem).ToList().ForEach(item => item.Selected = false);
                    OnMenuChanged(menuItem.Name);
                }

            }

        }

        public void SetMouseOverMenuItems(LeftMenuMouseOverEventArgs args)
        {
            MenuItemName menuItemName = args.MenuItemName;

            switch (menuItemName)
            {
                case MenuItemName.HighSchool:
                    HighSchoolMenuItemStyle.IsMouseOver = args.IsMouseOver;
                    break;
                case MenuItemName.Faculty:
                    FacultyMenuItemStyle.IsMouseOver = args.IsMouseOver;
                    break;
                case MenuItemName.Chair:
                    ChairMenuItemStyle.IsMouseOver = args.IsMouseOver;
                    break;
                case MenuItemName.Specialty:
                    SpecialtyMenuItemStyle.IsMouseOver = args.IsMouseOver;
                    break;
                case MenuItemName.Specialization:
                    SpecializationMenuItemStyle.IsMouseOver = args.IsMouseOver;
                    break;
            }

        }

        public void SetMouseUpMenuItems(MenuChangedEventArgs args)
        {
            MenuItemName menuItemName = args.MenuItemName;

            switch (menuItemName)
            {
                case MenuItemName.HighSchool:
                    HighSchoolMenuItemStyle.Selected = true;
                    break;
                case MenuItemName.Faculty:
                    FacultyMenuItemStyle.Selected = true;
                    break;
                case MenuItemName.Chair:
                    ChairMenuItemStyle.Selected = true;
                    break;
                case MenuItemName.Specialty:
                    SpecialtyMenuItemStyle.Selected = true;
                    break;
                case MenuItemName.Specialization:
                    SpecializationMenuItemStyle.Selected = true;
                    break;
            }

        }

        #endregion

        #region Events

        public delegate void MenuChangedEventHandler(object sender, MenuChangedEventArgs args);

        public event MenuChangedEventHandler MenuChanged;

        private void OnMenuChanged(MenuItemName menuName)
        {
            MenuChanged?.Invoke(this, new MenuChangedEventArgs(menuName));
        }

        #endregion
    }

}
