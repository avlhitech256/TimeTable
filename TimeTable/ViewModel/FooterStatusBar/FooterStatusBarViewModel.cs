using System;

namespace TimeTable.ViewModel.FooterStatusBar
{
    public class FooterStatusBarViewModel
    {
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
        public string ServerName { get; }

        #endregion

    }
}
