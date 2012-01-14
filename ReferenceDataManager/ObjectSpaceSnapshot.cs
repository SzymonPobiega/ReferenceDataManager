using System;
using Castle.DynamicProxy;

namespace ReferenceDataManager
{
    public class ObjectSpaceSnapshot : IObjectSpaceSnapshot
    {
        private readonly IDataFacade dataFacade;
        private readonly ObjectTypeDescriptorRepository objectTypeDescriptorRepository;
        private readonly ChangeSetId changeSetId;
        private static readonly ProxyGenerator proxyGenerator = new ProxyGenerator();
        private readonly ObjectIdentityMap identityMap = new ObjectIdentityMap();


        public ObjectSpaceSnapshot(IDataFacade dataFacade, ObjectTypeDescriptorRepository objectTypeDescriptorRepository, ChangeSetId changeSetId)
        {
            this.dataFacade = dataFacade;
            this.objectTypeDescriptorRepository = objectTypeDescriptorRepository;
            this.changeSetId = changeSetId;
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
                var objectState = dataFacade.GetById(changeSetId, objectId);
                var typeDescriptor = objectTypeDescriptorRepository.GetByTypeId(objectState.TypeId);
                proxy = CreateNewProxy(typeDescriptor.RuntimeType, objectState);
                identityMap.Put(objectId, proxy);
            }
            return proxy;
        }


        private object CreateNewProxy(Type objectType, ObjectState objectState)
        {
            var typeDescriptor = objectTypeDescriptorRepository.GetByTypeId(objectState.TypeId);
            var instance = CreateInstance(objectType);
            var interceptor = new ObjectStateManagementInterceptor(objectState, typeDescriptor, GetById);
            var proxy = proxyGenerator.CreateClassProxyWithTarget(objectType, instance, interceptor);
            return proxy;
        }

        private static object CreateInstance(Type objectType)
        {
            return Activator.CreateInstance(objectType);
        }
    }
}