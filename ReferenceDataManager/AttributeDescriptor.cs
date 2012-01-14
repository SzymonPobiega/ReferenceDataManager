namespace ReferenceDataManager
{
    public class AttributeDescriptor
    {
        private readonly string attributeName;
        private readonly string propertyName;

        public AttributeDescriptor(string attributeName, string propertyName)
        {
            this.attributeName = attributeName;
            this.propertyName = propertyName;
        }

        public string PropertyName
        {
            get { return propertyName; }
        }

        public string AttributeName
        {
            get { return attributeName; }
        }
    }
}