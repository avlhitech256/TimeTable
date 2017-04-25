using System;
using System.Collections;
using System.Data.Entity.Infrastructure;
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

        #endregion

        #region Constructors
        public BusinessLogicException() { }

        public BusinessLogicException(string message) : base(message) { }

        public BusinessLogicException(string message, System.Exception inner) : base(message, inner) { }

        protected BusinessLogicException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            if (info != null)
            {
                fieldName = info.GetString(nameof(FieldName));
                code = info.GetString(nameof(Code));
                value = info.GetString(nameof(Value));
                entity = info.GetString(nameof(Entity));
            }

        }

        #endregion

        #region Properties

        public string FieldName
        {
            private get { return fieldName; }
            set { fieldName = value; }
        }

        public string Code
        {
            private get { return code; }
            set { code = value; }
        }

        public string Value
        {
            private get { return value; }
            set { this.value = value; }
        }

        public string Entity
        {
            private get { return entity; }
            set { entity = value; }
        }

        #endregion

        #region Methods

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(FieldName), FieldName);
            info.AddValue(nameof(Code), Code);
            info.AddValue(nameof(Value), Value);
            info.AddValue(nameof(Entity), Entity);
        }

        #endregion
    }
}
