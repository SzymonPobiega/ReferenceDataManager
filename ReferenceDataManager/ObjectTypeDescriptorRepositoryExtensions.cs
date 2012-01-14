using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReferenceDataManager
{
    public static class ObjectTypeDescriptorRepositoryExtensions
    {
        public static ObjectTypeDescriptorRepository RegisterUsingReflection<T>(this ObjectTypeDescriptorRepository repository)
        {
            return RegisterUsingReflection(repository, typeof (T));
        }

        public static ObjectTypeDescriptorRepository RegisterUsingReflection(this ObjectTypeDescriptorRepository repository, Type typeToRegister)
        {
            var typeAttibute = typeToRegister
                .GetCustomAttributes(typeof (ObjectTypeAttribute), false)
                .Cast<ObjectTypeAttribute>()
                .Single();

            var objectTypeId = new ObjectTypeId(new Guid(typeAttibute.TypeIdValue));
            var typeDescritor = new ObjectTypeDescriptor(typeToRegister, objectTypeId, DiscoverAttributes(typeToRegister), DiscoverRelations(typeToRegister));

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

        private static IEnumerable<RelationDescriptor> DiscoverRelations(Type typeToRegister)
        {
            return typeToRegister
                .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.IsDefined(typeof(ObjectRelationAttribute), false))
                .Select(CreateRelationDescriptor);
        }

        private static RelationDescriptor CreateRelationDescriptor(PropertyInfo propertyInfo)
        {
            var metadata = propertyInfo.GetCustomAttributes(typeof(ObjectRelationAttribute), false).Cast<ObjectRelationAttribute>().Single();
            var propertyType = propertyInfo.PropertyType;
            var allowsMultipleValues = propertyType.IsGenericType
                                       && propertyType.GetGenericTypeDefinition() == typeof (IEnumerable<>);
            return new RelationDescriptor(metadata.RelationName ?? propertyInfo.Name, propertyInfo.Name, allowsMultipleValues);
        }
    }
}