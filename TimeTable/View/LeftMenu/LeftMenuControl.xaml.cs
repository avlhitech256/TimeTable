﻿using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using Common.Messenger;
using Common.Messenger.Impl;
using Domain.Data.Enum;
using Domain.DomainContext;
using Domain.Event;
using TimeTable.ViewModel.LeftMenu;

namespace TimeTable.View.LeftMenu
{
    /// <summary>
    /// Логика взаимодействия для LeftMenuControl.xaml
    /// </summary>
    public partial class LeftMenuControl : UserControl, IDisposable
    {
        #region Members

        private readonly Dictionary<Border, MenuItemName> borderMap;
        private IDomainContext context;

        #endregion

        #region Constructors

        public LeftMenuControl()
        {
            InitializeComponent();
            borderMap = 
                new Dictionary<Border, MenuItemName>
                {
                    { HighSchoolButton, MenuItemName.HighSchool },
                    { FacultyButton, MenuItemName.Faculty},
                    { ChairButton, MenuItemName.Chair },
                    { SpecialtyButton, MenuItemName.Specialty },
                    { SpecializationButton, MenuItemName.Specialization },
                    { EmployeeButton, MenuItemName.Employee }
                };
        }

        #endregion

        #region Properties

        public IDomainContext DomainContext
        {
            get
            {
                return context;
            }

            set
            {
                if (context == null || context != value)
                {
                    context = value;
                    InitializeDataContext();
                }
            }

        }
        private IMessenger Messenger => DomainContext?.Messenger;
        public LeftMenuViewModel ViewModel { get; private set; }

        #endregion

        #region Methods

        private void InitializeDataContext()
        {
            ViewModel = new LeftMenuViewModel(DomainContext);
            DataContext = ViewModel;
        }

        private LeftMenuMouseOverEventArgs CreateLeftMenuMouseOverEventArgs(Border border, bool isMouseOver)
        {
            LeftMenuMouseOverEventArgs args = null;

            if (border != null && borderMap != null && borderMap.ContainsKey(border))
            {
                args = new LeftMenuMouseOverEventArgs(borderMap[border], isMouseOver);
            }

            return args;
        }

        private LeftMenuMouseOverEventArgs CreateLeftMenuMouseOverEventArgs(object sender, bool isMouseOver)
        {
            LeftMenuMouseOverEventArgs args = null;

            Border button = sender as Border;

            if (button != null)
            {
                args = CreateLeftMenuMouseOverEventArgs(button, isMouseOver);
            }

            return args;
        }

        private void SetMouseOver(object sender, bool isMouseOver)
        {

            LeftMenuMouseOverEventArgs args = CreateLeftMenuMouseOverEventArgs(sender, isMouseOver);

            if (args != null)
            {
                ViewModel.MenuItemsStyle.SetMouseOverMenuItems(args);
            }

        }

        private void Border_OnMouseEnter(object sender, MouseEventArgs e)
        {
            SetMouseOver(sender, true);
        }

        private void Border_OnMouseLeave(object sender, MouseEventArgs e)
        {
            SetMouseOver(sender, false);
        }

        private MenuChangedEventArgs CreateMenuChangedEventArgs(Border border)
        {
            MenuChangedEventArgs args = null;

            if (border != null && borderMap != null && borderMap.ContainsKey(border))
            {
                args = new MenuChangedEventArgs(borderMap[border]);
            }

            return args;
        }

        private MenuChangedEventArgs CreateMenuChangedEventArgs(object sender)
        {
            MenuChangedEventArgs args = null;
            Border button = sender as Border;

            if (button != null)
            {
                args = CreateMenuChangedEventArgs(button);
            }

            return args;
        }

        private void SetMouseUp(object sender)
        {
            MenuChangedEventArgs args = CreateMenuChangedEventArgs(sender);

            if (args != null)
            {
                Messenger?.Send(CommandName.SetEntryControl, args);
            }

        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            SetMouseUp(sender);
        }

        public void Dispose()
        {
            borderMap.Clear();
        }

        #endregion

    }

}
