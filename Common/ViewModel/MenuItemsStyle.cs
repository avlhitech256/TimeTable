using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Common.Data.Notifier;

namespace Common.ViewModel
{
    public class MenuItemsStyle : Notifier
    {
        #region Members

        private Brush notSelectedAndMouseIsNotOverBrush;
        private Brush notSelectedAndMouseIsOverBrush;
        private Brush selectedAndMouseIsNotOverBrush;
        private Brush selectedAndMouseIsOverBrush;

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

        public MenuItemsStyle(Brush notSelectedAndMouseIsNotOverBrush,
                              Brush notSelectedAndMouseIsOverBrush,
                              Brush selectedAndMouseIsNotOverBrush,
                              Brush selectedAndMouseIsOverBrush)
        {
            this.notSelectedAndMouseIsNotOverBrush = notSelectedAndMouseIsNotOverBrush;
            this.notSelectedAndMouseIsOverBrush = notSelectedAndMouseIsOverBrush;
            this.selectedAndMouseIsNotOverBrush = selectedAndMouseIsNotOverBrush;
            this.selectedAndMouseIsOverBrush = selectedAndMouseIsOverBrush;

            highSchoolMenuItemStyle = new MenuItemStyle(notSelectedAndMouseIsNotOverBrush,
                                                        notSelectedAndMouseIsOverBrush, 
                                                        selectedAndMouseIsNotOverBrush, 
                                                        selectedAndMouseIsOverBrush);
            facultyMenuItemStyle = new MenuItemStyle(notSelectedAndMouseIsNotOverBrush,
                                                     notSelectedAndMouseIsOverBrush,
                                                     selectedAndMouseIsNotOverBrush,
                                                     selectedAndMouseIsOverBrush);
            chairMenuItemStyle = new MenuItemStyle(notSelectedAndMouseIsNotOverBrush,
                                                   notSelectedAndMouseIsOverBrush,
                                                   selectedAndMouseIsNotOverBrush,
                                                   selectedAndMouseIsOverBrush);
            specialtyMenuItemStyle = new MenuItemStyle(notSelectedAndMouseIsNotOverBrush,
                                                       notSelectedAndMouseIsOverBrush,
                                                       selectedAndMouseIsNotOverBrush,
                                                       selectedAndMouseIsOverBrush);
            specializationMenuItemStyle = new MenuItemStyle(notSelectedAndMouseIsNotOverBrush,
                                                            notSelectedAndMouseIsOverBrush,
                                                            selectedAndMouseIsNotOverBrush,
                                                            selectedAndMouseIsOverBrush);

            menuItems = new List<MenuItemStyle>();

            AddMenuItem(highSchoolMenuItemStyle);
            AddMenuItem(facultyMenuItemStyle);
            AddMenuItem(chairMenuItemStyle);
            AddMenuItem(specialtyMenuItemStyle);
            AddMenuItem(specializationMenuItemStyle);
        }

        #endregion

        #region Properties

        public Brush NotSelectedAndMouseIsNotOverBrush
        {
            get
            {
                return notSelectedAndMouseIsNotOverBrush;
            }

            set
            {
                if (notSelectedAndMouseIsNotOverBrush == null || notSelectedAndMouseIsNotOverBrush.Equals(value))
                {
                    notSelectedAndMouseIsNotOverBrush = value;
                    OnPropertyChanged();
                    menuItems.ForEach(
                        item =>
                        {
                            item.NotSelectedAndMouseIsNotOverBrush = NotSelectedAndMouseIsNotOverBrush;
                        });
                }
            }
        }

        public Brush NotSelectedAndMouseIsOverBrush
        {
            get
            {
                return notSelectedAndMouseIsOverBrush;
            }

            set
            {
                if (notSelectedAndMouseIsOverBrush == null || notSelectedAndMouseIsOverBrush.Equals(value))
                {
                    notSelectedAndMouseIsOverBrush = value;
                    OnPropertyChanged();
                    menuItems.ForEach(
                        item =>
                        {
                            item.NotSelectedAndMouseIsOverBrush = NotSelectedAndMouseIsOverBrush;
                        });
                }

            }

        }

        public Brush SelectedAndMouseIsNotOverBrush
        {
            get
            {
                return selectedAndMouseIsNotOverBrush;
            }

            set
            {
                if (selectedAndMouseIsNotOverBrush == null || selectedAndMouseIsNotOverBrush.Equals(value))
                {
                    selectedAndMouseIsNotOverBrush = value;
                    OnPropertyChanged();
                    menuItems.ForEach(
                        item =>
                        {
                            item.SelectedAndMouseIsNotOverBrush = SelectedAndMouseIsNotOverBrush;
                        });
                }

            }

        }

        public Brush SelectedAndMouseIsOverBrush
        {
            get
            {
                return selectedAndMouseIsOverBrush;
            }

            set
            {
                if (selectedAndMouseIsOverBrush == null || selectedAndMouseIsOverBrush.Equals(value))
                {
                    selectedAndMouseIsOverBrush = value;
                    OnPropertyChanged();
                    menuItems.ForEach(
                        item =>
                        {
                            item.SelectedAndMouseIsOverBrush = SelectedAndMouseIsOverBrush;
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
                }

            }

        }

        #endregion

    }

}
