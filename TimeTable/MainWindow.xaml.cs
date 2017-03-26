using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Common.Data.Enum;
using Common.DomainContext;
using Common.Entry;
using Common.Event;
using Common.Messenger;
using Common.Messenger.Impl;
using HighSchool.View;

namespace TimeTable
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Members

        private DomainContext context;

        #endregion

        #region Constructors

        public MainWindow()
        {
            List<string> picturesList = CreatePicturesList();
            SplashScreen splashScreen = CreateSplashScreen(picturesList);
            
            InitializeComponent();
            context = new DomainContext();
            DataContext = context;
            PopulateFooterBar();
            SubscribeLeftMenu();
            SubscribeMessenger();


            if (splashScreen != null)
            {
                //Hide();
                //Thread.Sleep(3000);
                DateTime now = DateTime.Now.AddSeconds(3);
                splashScreen.Close(TimeSpan.FromSeconds(1));

                while (DateTime.Now < now)
                {

                }

            }

        }

        #endregion

        #region Methods

        private void SubscribeLeftMenu()
        {
            context.MainViewModel.MenuItemsStyle.MenuChanged += OnChangedLeftMenu;
        }

        private void OnChangedLeftMenu(object sender, MenuChangedEventArgs args)
        {
            switch (args.MenuItemName)
            {
                case MenuItemName.HighSchool:
                    EntryControl.Content = new HighSchoolSearchControl();
                    break;
                case MenuItemName.Faculty:
                    EntryControl.Content = null;
                    break;
                case MenuItemName.Chair:
                    EntryControl.Content = null;
                    break;
                case MenuItemName.Specialty:
                    EntryControl.Content = null;
                    break;
                case MenuItemName.Specialization:
                    EntryControl.Content = null;
                    break;
                default:
                    EntryControl.Content = null;
                    break;
            }

        }

        private void SubscribeMessenger()
        {
            context.Messenger.Register<EntryControl>(CommandName.SetEntryControl, SetEntryControl, (x) => true);
        }

        private void SetEntryControl(EntryControl entryControl)
        {
            if (entryControl is HighSchoolSearchControl)
            {
                EntryControl.Content = (HighSchoolSearchControl) entryControl;
            }
            else
            {
                EntryControl.Content = entryControl;
            }
        }

        private void PopulateFooterBar()
        {
            FooterBarControl.UserTextBox.Text = Environment.UserName;
            FooterBarControl.WorkstationTextBox.Text = Environment.MachineName;
        }

        private List<string> CreatePicturesList()
        {
            List<string> result = new List<string>();

            result.Add("Pictures/ipsa0.gif");
            result.Add("Pictures/ipsa1.jpg");
            result.Add("Pictures/ipsa10.jpg");
            result.Add("Pictures/ipsa11.jpg");
            result.Add("Pictures/ipsa11.png");
            result.Add("Pictures/ipsa12.jpg");
            result.Add("Pictures/ipsa12.png");
            result.Add("Pictures/ipsa13.jpg");
            result.Add("Pictures/ipsa13.png");
            result.Add("Pictures/ipsa14.jpg");
            result.Add("Pictures/ipsa14.png");
            result.Add("Pictures/ipsa15.jpg");
            result.Add("Pictures/ipsa15.png");
            result.Add("Pictures/ipsa16.jpg");
            result.Add("Pictures/ipsa16.png");
            result.Add("Pictures/ipsa17.jpg");
            result.Add("Pictures/ipsa17.png");
            result.Add("Pictures/ipsa18.jpg");
            result.Add("Pictures/ipsa18.png");
            result.Add("Pictures/ipsa19.jpg");
            result.Add("Pictures/ipsa2.jpg");
            result.Add("Pictures/ipsa20.jpg");
            result.Add("Pictures/ipsa21.jpg");
            result.Add("Pictures/ipsa22.jpg");
            result.Add("Pictures/ipsa23.jpg");
            result.Add("Pictures/ipsa24.jpg");
            result.Add("Pictures/ipsa25.jpg");
            result.Add("Pictures/ipsa26.jpg");
            result.Add("Pictures/ipsa3.png");
            result.Add("Pictures/ipsa4.JPG");
            result.Add("Pictures/ipsa5.jpg");
            result.Add("Pictures/ipsa6.jpg");
            result.Add("Pictures/ipsa7.jpg");
            result.Add("Pictures/ipsa7.png");
            result.Add("Pictures/ipsa8.jpg");
            result.Add("Pictures/ipsa9.png");

            return result;
        }

        private SplashScreen CreateSplashScreen(List<string> picturesList)
        {
            SplashScreen splashScreen = null;
            List<int> indexList = new List<int>();
            bool isNotCreatedSplashScreen = true;
            int currentSeconds = DateTime.Now.Second;

            while (isNotCreatedSplashScreen && indexList.Count < picturesList.Count)
            {
                int index = currentSeconds % picturesList.Count;

                if (!indexList.Contains(index))
                {
                    indexList.Add(index);
                    string pathPicture = picturesList[index];
                    splashScreen = new SplashScreen(pathPicture);

                    try
                    {
                        isNotCreatedSplashScreen = false;
                        splashScreen.Show(false);
                    }
                    catch (Exception)
                    {
                        isNotCreatedSplashScreen = true;
                    }

                }

            }

            return splashScreen;
        }

        #endregion
    }

}
