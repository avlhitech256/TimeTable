using System;
using Common.Data.Notifier;
using Domain.Messenger;
using Domain.Entry;


namespace Domain.DomainContext
{
    public class DomainContext : Notifier, IDomainContext
    {
        #region Members

        private IViewModel viewModel;
        private string dataBaseServer;

        #endregion

        #region Constructors

        public DomainContext()
        {
            Messenger = new Common.Messenger.Impl.Messenger();
            UserName = Environment.UserName;
            UserDomain = Environment.UserDomainName;
            Workstation = Environment.MachineName;
            DataBaseServer = string.Empty;
        }

        #endregion

        #region Properties

        public IMessenger Messenger { get; }
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
        public string DataBaseServer
        {
            get
            {
                return dataBaseServer;
            }

            set
            {
                if (dataBaseServer != value)
                {
                    dataBaseServer = value;
                    OnPropertyChanged();
                }

            }

        }


        #endregion

    }

}
