using Common.Data.Notifier;
using Domain.Messenger;
using Domain.Messenger.Impl;
using Domain.DomainContext;
using Domain.Event;

namespace TimeTable.ViewModel.MainWindow
{

    public class MainWindowViewModel : Notifier
    {
        #region Members

        private object view;
        private readonly IDomainContext context;

        #endregion

        #region Constructors

        public MainWindowViewModel(IDomainContext domainContext)
        {
            context = domainContext;
            ViewModelRouter = new ViewModelRouter(context);
            SubscribeMessenger();
        }

        #endregion


        #region Properties

        private IMessenger Messenger => context?.Messenger;
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
            ViewFactory viewFactory = new ViewFactory(ViewModelRouter);
            View = viewFactory.GetView(args.MenuItemName);
        }

        private bool CanSetEntryControl(MenuChangedEventArgs args)
        {
            return true;
        }



        #endregion

    }

}
