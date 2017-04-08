using System;
using Common.Data.Notifier;
using Domain.Messenger;
using DataService.DataService;
using Domain.Entry;


namespace Domain.DomainContext
{
    public class DomainContext : Notifier, IDomainContext
    {
        #region Members

        private IViewModel viewModel;

        #endregion

        #region Constructors

        public DomainContext()
        {
            Messenger = new Common.Messenger.Impl.Messenger();
            DataService = new DataService.DataService.DataService();
            UserName = Environment.UserName;
            UserDomain = Environment.UserDomainName;
            Workstation = Environment.MachineName;
            DataBaseServer = DataService.DBContext.Database.Connection.DataSource;
        }

        #endregion

        #region Properties

        public IMessenger Messenger { get; }
        public IDataService DataService { get; }

        public IViewModel ViewModel
        {
            get
            {
                return viewModel;
            }

            set
            {
                if (viewModel != value)
                {
                    viewModel = value;
                    OnPropertyChanged();
                }
            }

        }
        public string UserName { get; }
        public string UserDomain { get; }
        public string Workstation { get; }
        public string DataBaseServer { get; }


        #endregion

    }

}
