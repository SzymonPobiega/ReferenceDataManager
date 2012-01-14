using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;

namespace ReferenceDataManager
{
    public class ObjectStateManagementInterceptor : IInterceptor
    {
        private readonly ObjectState objectState;
        private readonly ObjectTypeDescriptor objectTypeDescriptor;
        private readonly Func<ObjectId, object> hydrateCallback;

        public ObjectStateManagementInterceptor(ObjectState objectState, ObjectTypeDescriptor objectTypeDescriptor, Func<ObjectId, object> hydrateCallback)
        {
            this.objectState = objectState;
            this.objectTypeDescriptor = objectTypeDescriptor;
            this.hydrateCallback = hydrateCallback;
        }

        public void Intercept(IInvocation invocation)
        {
            if (IsPropertyGetMethod(invocation))
            {
                var property = FindProperty(invocation);
                if (IsMappedToAttribute(property))
                {
                    invocation.ReturnValue = GetAttributeValue(property);
                }
                else if (IsMappedToRelation(property))
                {
                    invocation.ReturnValue = GetRelationValue(property);
                }
                else
                {
                    invocation.Proceed();
                }
            }
            else
            {
                invocation.Proceed();                
            }
        }
        
        private bool IsMappedToRelation(PropertyInfo propertyInfo)
        {
            return objectTypeDescriptor.GetRelationByPropertyName(propertyInfo.Name) != null;
        }

        private bool IsMappedToAttribute(PropertyInfo propertyInfo)
        {
            return objectTypeDescriptor.GetAttributeByPropertyName(propertyInfo.Name) != null;
        }

        private static PropertyInfo FindProperty(IInvocation invocation)
        {
            var propertyName = invocation.Method.Name.Substring(4);
            return invocation.TargetType.GetProperty(propertyName,
                                                     BindingFlags.Public | BindingFlags.NonPublic |
                                                     BindingFlags.Instance);
        }

        private object GetAttributeValue(PropertyInfo propertyInfo)
        {
            var attributeDescriptor = objectTypeDescriptor.GetAttributeByPropertyName(propertyInfo.Name);
            var attributeValue = objectState.GetAttributeValue(attributeDescriptor.AttributeName);
            var returnType = propertyInfo.PropertyType;
            if (returnType.IsValueType && attributeValue == null)
            {
                attributeValue = Activator.CreateInstance(returnType);
            }
            return attributeValue;
        }

        private object GetRelationValue(PropertyInfo propertyInfo)
        {
            var relationDescriptor = objectTypeDescriptor.GetRelationByPropertyName(propertyInfo.Name);
            var relatedObjectIds = objectState.GetRelated(relationDescriptor.RelationName);
            var untypedData = relatedObjectIds.Select(x => hydrateCallback(x));

            return AdaptReturnType(untypedData, relationDescriptor, propertyInfo.PropertyType);

        }

        private static object AdaptReturnType(IEnumerable<object> untypedData, RelationDescriptor relationDescriptor, Type propertyType)
        {
            return relationDescriptor.AllowsMultipleValues
                       ? AdaptMultiValueRelation(untypedData, propertyType)
                       : untypedData.FirstOrDefault();
        }

        private static object AdaptMultiValueRelation(IEnumerable<object> untypedData, Type propertyType)
        {
            var elementType = propertyType.GetGenericArguments()[0];
            var adapterType = typeof (RelationCollecationAdapter<>).MakeGenericType(elementType);
            var adapter = Activator.CreateInstance(adapterType, untypedData);
            return adapter;
        }

        private static bool IsPropertyGetMethod(IInvocation invocation)
        {
            return invocation.Method.IsSpecialName && invocation.Method.Name.StartsWith("get_");
        }

        public class RelationCollecationAdapter<T> : IEnumerable<T>
        {
            private readonly IEnumerable<T> adaptedData;

            public RelationCollecationAdapter(IEnumerable<object> adaptedData)
            {
                this.adaptedData = adaptedData.Cast<T>();
            }

            public IEnumerator<T> GetEnumerator()
            {
                return adaptedData.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}