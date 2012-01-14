using System;
using System.Collections.Generic;

namespace ReferenceDataManager
{
    public class ObjectTypeDescriptorRepository
    {
        private readonly Dictionary<ObjectTypeId, ObjectTypeDescriptor> map = new Dictionary<ObjectTypeId, ObjectTypeDescriptor>();

        public ObjectTypeDescriptorRepository RegisterTypeDescriptor(ObjectTypeDescriptor typeDescriptor)
        {
            if (map.ContainsKey(typeDescriptor.ObjectTypeId))
            {
                throw new InvalidOperationException(string.Format("Another type with same id {0} has already been registered.", typeDescriptor.ObjectTypeId));
            }
            map[typeDescriptor.ObjectTypeId] = typeDescriptor;
            return this;
        }

        public ObjectTypeDescriptor GetByTypeId(ObjectTypeId objectTypeId)
        {
            ObjectTypeDescriptor existing;
            return map.TryGetValue(objectTypeId, out existing) ? existing : null;
        }
    }
}