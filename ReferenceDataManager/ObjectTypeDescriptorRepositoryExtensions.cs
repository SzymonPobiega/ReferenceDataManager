using System;
using System.Linq;

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
            var typeDescritor = new ObjectTypeDescriptor(typeToRegister, objectTypeId, new AttributeDescriptor[] {},
                                                         new RelationDescriptor[] {});

            repository.RegisterTypeDescriptor(typeDescritor);
            return repository;
        }
    }
}