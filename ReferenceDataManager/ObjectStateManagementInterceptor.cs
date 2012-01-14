using Castle.DynamicProxy;

namespace ReferenceDataManager
{
    public class ObjectStateManagementInterceptor : IInterceptor
    {
        private readonly ObjectState objectState;
        private readonly IDataFacade dataFacade;
        private readonly ChangeSetId changeSetId;

        public ObjectStateManagementInterceptor(ObjectState objectState, IDataFacade dataFacade, ChangeSetId changeSetId)
        {
            this.objectState = objectState;
            this.dataFacade = dataFacade;
            this.changeSetId = changeSetId;
        }

        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
        }
    }
}