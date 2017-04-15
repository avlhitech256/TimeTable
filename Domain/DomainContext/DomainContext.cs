using System;
using Domain.Annotations;
using Domain.Data.Notifier;
using Domain.Messenger;
using Domain.ViewModel;


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
            Messenger = new Domain.Messenger.Impl.Messenger();
            UserName = Environment.UserName;
            UserDomain = Environment.UserDomainName;
            Workstation = Environment.MachineName;
            DataBaseServer = string.Empty;
        }

        #endregion

        #region Properties

        [CanBeNull]
        public IMessenger Messenger { get; }

        [CanBeNull]
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

        [CanBeNull]
        public string UserName { get; }

        [CanBeNull]
        public string UserDomain { get; }

        [CanBeNull]
        public string Workstation { get; }

        [CanBeNull]
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
