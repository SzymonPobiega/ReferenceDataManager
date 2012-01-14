using System;
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
            if (IsPropertyGetMethod(invocation))
            {
                var attributeName = invocation.Method.Name.Substring(4);
                var attributeValue = GetAttributeValue(invocation, attributeName);
                invocation.ReturnValue = attributeValue;
            }
            else
            {
                invocation.Proceed();                
            }
        }

        private object GetAttributeValue(IInvocation invocation, string attributeName)
        {
            var attributeValue = objectState.GetAttributeValue(attributeName);
            var returnType = invocation.Method.ReturnType;
            if (returnType.IsValueType && attributeValue == null)
            {
                attributeValue = Activator.CreateInstance(returnType);
            }
            return attributeValue;
        }

        private static bool IsPropertyGetMethod(IInvocation invocation)
        {
            return invocation.Method.IsSpecialName && invocation.Method.Name.StartsWith("get_");
        }
    }
}