using System;
using System.Runtime.Serialization;

namespace DataService.Exception
{
    [Serializable]
    public class BusinessLogicException : System.Exception
    {
        #region Members

        private string fieldName;
        private string code;
        private string value;
        private string entity;
        private string _message;

        #endregion

        #region Constructors
        public BusinessLogicException() { }

        public BusinessLogicException(string message) : base(message) { }

        public BusinessLogicException(string message, System.Exception inner) : base(message, inner) { }

        protected BusinessLogicException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        #endregion
    }
}
