using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReferenceDataManager
{
    public static class ObjectTypeDescriptorRepositoryExtensions
    {
        public static ObjectTypeDescriptorRepository RegisterUsingReflection(this ObjectTypeDescriptorRepository repository, Type typeToRegister)
        {
            var typeAttibute = typeToRegister
                .GetCustomAttributes(typeof (ObjectTypeAttribute), false)
                .Cast<ObjectTypeAttribute>()
                .Single();

            var objectTypeId = new ObjectTypeId(new Guid(typeAttibute.TypeIdValue));
            var typeDescritor = new ObjectTypeDescriptor(typeToRegister, objectTypeId, DiscoverAttributes(typeToRegister),
                                                         new RelationDescriptor[] {});

            repository.RegisterTypeDescriptor(typeDescritor);
            return repository;
        }

        private static IEnumerable<AttributeDescriptor> DiscoverAttributes(Type typeToRegister)
        {
            return typeToRegister
                .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.IsDefined(typeof (ObjectAttributeAttribute), false))
                .Select(CreateAttributeDescriptor);
        }

        private static AttributeDescriptor CreateAttributeDescriptor(PropertyInfo propertyInfo)
        {
            var metadata = propertyInfo.GetCustomAttributes(typeof(ObjectAttributeAttribute), false).Cast<ObjectAttributeAttribute>().Single();
            return new AttributeDescriptor(metadata.AttibuteName ?? propertyInfo.Name, propertyInfo.Name);
        }
    }
}