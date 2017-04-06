using System;
using Common.Data.Notifier;
using Domain.DomainContext;

namespace TimeTable.ViewModel.FooterStatusBar
{
    public class FooterStatusBarViewModel : Notifier
    {
        #region 

        private IDomainContext context;
        private string server;

        #endregion

        #region Constructors

        public FooterStatusBarViewModel()
        {
            UserName = Environment.UserName;
            UserDomain = Environment.UserDomainName;
            MachineName = Environment.MachineName;
            ServerName = string.Empty;
        }

        #endregion

        #region Properties

        public string UserName { get; }
        public string UserDomain { get; }
        public string MachineName { get; }

        public string ServerName
        {
            get
            {
                return server;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(server) || server != value)
                {
                    server = value;
                    OnPropertyChanged();
                }

            }

        }

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
                    ServerName = DomainContext.DataService.DBContext.Database.Connection.DataSource;
                }
            }

        }

        #endregion

    }
}
