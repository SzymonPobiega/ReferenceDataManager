using System;
using Castle.DynamicProxy;

namespace ReferenceDataManager
{
    public class ObjectSpaceSnapshot : IObjectSpaceSnapshot
    {
        private readonly IDataFacade dataFacade;
        private readonly ChangeSetId changeSetId;
        private static readonly ProxyGenerator proxyGenerator = new ProxyGenerator();

        public ObjectSpaceSnapshot(IDataFacade dataFacade, ChangeSetId changeSetId)
        {
            this.dataFacade = dataFacade;
            this.changeSetId = changeSetId;
        }

        public T GetById<T>(ObjectId objectId)
        {
            return (T)GetById(typeof (T), objectId);
        }

        public object GetById(Type objectType, ObjectId objectId)
        {
            var objectState = dataFacade.GetById(changeSetId, objectId);
            var instance = CreateInstance(objectType);
            var interceptor = new ObjectStateManagementInterceptor(objectState, dataFacade, changeSetId);
            var proxy = proxyGenerator.CreateClassProxyWithTarget(objectType, instance, interceptor);
            return proxy;
        }

        private static object CreateInstance(Type objectType)
        {
            return Activator.CreateInstance(objectType);
        }
    }
}