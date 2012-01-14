using System;

namespace ReferenceDataManager
{
    public static class ObjectTypeDescriptorRepositoryExtensions
    {
        public static ObjectTypeDescriptorRepository RegisterUsingReflection(this ObjectTypeDescriptorRepository repository, Type typeToRegister)
        {
            return repository;
        }
    }
}