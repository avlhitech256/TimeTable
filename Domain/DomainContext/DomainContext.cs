using System;
using Common.Data.Notifier;
using Common.Annotations;
using Common.Messenger;
using Domain.ViewModel;


namespace Common.DomainContext
{
    public class DomainContext : Notifier, IDomainContext
    {
        #region Members

        private IControlViewModel viewModel;
        private string dataBaseServer;

        #endregion

        #region Constructors

        public DomainContext()
        {
            Messenger = new global::Common.Messenger.Impl.Messenger();
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
        public IControlViewModel ViewModel
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
