using System;
using System.Runtime.Serialization;

namespace Common.Exception
{
    [Serializable]
    public class BusinessLogicException : System.Exception
    {
        #region Constructors
        public BusinessLogicException() { }

        public BusinessLogicException(string message) : base(message) { }

        public BusinessLogicException(string message, System.Exception inner) : base(message, inner) { }

        protected BusinessLogicException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        #endregion
    }
}
