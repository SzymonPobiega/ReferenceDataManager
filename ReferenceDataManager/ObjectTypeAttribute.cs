using System;

namespace ReferenceDataManager
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class ObjectTypeAttribute : Attribute
    {
        private readonly string typeIdValue;

        public ObjectTypeAttribute(string typeIdValue)
        {
            this.typeIdValue = typeIdValue;
        }

        public string TypeIdValue
        {
            get { return typeIdValue; }
        }
    }
}