using System;

namespace ReferenceDataManager
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class ObjectAttributeAttribute : Attribute
    {
        private readonly string attibuteName;

        public ObjectAttributeAttribute(string attibuteName)
        {
            this.attibuteName = attibuteName;
        }

        public ObjectAttributeAttribute()
        {
        }

        public string AttibuteName
        {
            get { return attibuteName; }
        }
    }
}