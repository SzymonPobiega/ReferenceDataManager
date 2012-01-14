namespace ReferenceDataManager
{
    public class RelationDescriptor
    {
        private readonly string relationName;
        private readonly string propertyName;
        private readonly bool allowsMultipleValues;

        public RelationDescriptor(string relationName, string propertyName, bool allowsMultipleValues)
        {
            this.relationName = relationName;
            this.allowsMultipleValues = allowsMultipleValues;
            this.propertyName = propertyName;
        }

        public bool AllowsMultipleValues
        {
            get { return allowsMultipleValues; }
        }

        public string PropertyName
        {
            get { return propertyName; }
        }

        public string RelationName
        {
            get { return relationName; }
        }
    }
}