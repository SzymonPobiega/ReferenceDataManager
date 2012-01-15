using System;
using Castle.DynamicProxy;

namespace ReferenceDataManager
{
    public class ObjectSpaceSnapshot : IObjectSpaceSnapshot
    {
        private readonly IDataRetrievalStrategy dataRetrievalStrategy;
        private readonly ObjectTypeDescriptorRepository objectTypeDescriptorRepository;
        private static readonly ProxyGenerator proxyGenerator = new ProxyGenerator();
        private readonly ObjectIdentityMap identityMap = new ObjectIdentityMap();

        
        public ObjectSpaceSnapshot(ObjectTypeDescriptorRepository objectTypeDescriptorRepository, IDataRetrievalStrategy dataRetrievalStrategy)
        {
            this.objectTypeDescriptorRepository = objectTypeDescriptorRepository;
            this.dataRetrievalStrategy = dataRetrievalStrategy;
        }

        public T GetById<T>(ObjectId objectId)
        {
            return (T)GetById(objectId);
        }

        public object GetById(ObjectId objectId)
        {
            var proxy = identityMap.GetById(objectId);
            if (proxy == null)
            {
                var objectState = RetrieveData(objectId);
                var typeDescriptor = objectTypeDescriptorRepository.GetByTypeId(objectState.TypeId);
                proxy = CreateNewProxy(typeDescriptor, objectState);
                identityMap.Put(objectId, proxy);
            }
            return proxy;
        }

        protected virtual ObjectState RetrieveData(ObjectId objectId)
        {
            return dataRetrievalStrategy.GetById(objectId);
        }


        private object CreateNewProxy(ObjectTypeDescriptor typeDescriptor, ObjectState objectState)
        {
            var interceptor = new ObjectStateManagementInterceptor(objectState, typeDescriptor, GetById);
            var proxy = proxyGenerator.CreateClassProxy(typeDescriptor.RuntimeType, interceptor);
            return proxy;
        }
    }
}