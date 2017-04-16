using System;
using System.Data.Entity.Core;

namespace Common.Event
{
    public class EntityExceptionEventArgs : EventArgs
    {
        #region Constructors

        public EntityExceptionEventArgs(EntityException entityException)
        {
            EntityException = entityException;
        }

        #endregion

        #region Properties

        public EntityException EntityException { get; }

        #endregion

    }

    public delegate void EntityExceptionEventHandler(object sender, EntityExceptionEventArgs e);
}
