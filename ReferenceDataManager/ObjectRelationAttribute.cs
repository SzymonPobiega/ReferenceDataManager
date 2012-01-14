using System;

namespace ReferenceDataManager
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class ObjectRelationAttribute : Attribute
    {
        private readonly string relationName;

        public ObjectRelationAttribute(string relationName)
        {
            this.relationName = relationName;
        }

        public ObjectRelationAttribute()
        {
        }

        public string RelationName
        {
            get { return relationName; }
        }
    }
}