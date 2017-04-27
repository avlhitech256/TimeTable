using Common.Data.Notifier;
using Common.Messenger;
using Common.Messenger.Impl;
using Domain.DomainContext;
using Domain.Event;

namespace TimeTable.ViewModel.MainWindow
{

    public class MainWindowViewModel : Notifier
    {
        #region Members

        private object view;

        #endregion

        #region Constructors

        public MainWindowViewModel(IDomainContext domainContext)
        {
            DomainContext = domainContext;
            ViewModelRouter = new ViewModelRouter(DomainContext);
            SubscribeMessenger();
        }

        #endregion


        #region Properties

        private IDomainContext DomainContext { get; }
        private IMessenger Messenger => DomainContext?.Messenger;
        private ViewModelRouter ViewModelRouter { get; }

        public object View
        {
            get
            {
                return view;
            }

            set
            { 
                if (view != value)
                {
                    view = value;
                    OnPropertyChanged();
                }

            }

        }

        #endregion

        #region Methods

        private void SubscribeMessenger()
        {
            Messenger?.Register<MenuChangedEventArgs>(CommandName.SetEntryControl, SetEntryControl, CanSetEntryControl);
        }

        public void SetEntryControl(MenuChangedEventArgs args)
        {
            var viewFactory = new ViewFactory(DomainContext, ViewModelRouter);
            View = viewFactory.GetView(args.MenuItemName);
        }

        private bool CanSetEntryControl(MenuChangedEventArgs args)
        {
            return true;
        }



        #endregion

    }

}
