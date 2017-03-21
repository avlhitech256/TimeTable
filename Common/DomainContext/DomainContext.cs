using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DomainContext
{
    public class DomainContext
    {
        #region Members

        private static DomainContext context;

        #endregion
        private DomainContext()
        {
            
        }

        public static DomainContext Instance()
        {
            return context ?? (context = new DomainContext());
        }
    }
}
