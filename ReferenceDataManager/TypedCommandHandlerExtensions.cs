using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace ReferenceDataManager
{
    public static class TypedCommandHandlerExtensions
    {
        public static void Create<TCommand, TTarget>(this ICommandExecutionContext context, ITypedCommandHandler<TCommand, TTarget> handler)
            where TCommand : TypedCommand<TTarget>
        {
            var typeAttribute = AttributeBasedObjectTypeMapping.GetTypeAttibute(typeof (TTarget));
            context.Create(ObjectTypeId.Parse(typeAttribute.TypeIdValue));
        }

        public static IEnumerable<ObjectId> GetRelated<TCommand, TTarget, TValue>(this ICommandExecutionContext context, ITypedCommandHandler<TCommand, TTarget> handler, Expression<Func<TTarget, IEnumerable<TValue>>> propertyExpression)
            where TCommand : TypedCommand<TTarget>
        {
            return context.GetRelated(GetRelationName(propertyExpression));
        }

        public static IEnumerable<ObjectId> GetRelated<TCommand, TTarget, TValue>(this ICommandExecutionContext context, ITypedCommandHandler<TCommand, TTarget> handler, Expression<Func<TTarget, TValue>> propertyExpression)
            where TCommand : TypedCommand<TTarget>
        {
            return context.GetRelated(GetRelationName(propertyExpression));
        }

        public static void ModifyAttribute<TCommand, TTarget, TValue>(this ICommandExecutionContext context, ITypedCommandHandler<TCommand, TTarget> handler, Expression<Func<TTarget, TValue>> propertyExpression, TValue value)
            where TCommand : TypedCommand<TTarget>
        {
            var propertyInfo = GetPropertyInfo(propertyExpression);
            var attributeDescriptor = AttributeBasedObjectTypeMapping.CreateAttributeDescriptor(propertyInfo);

            context.ModifyAttribute(attributeDescriptor.AttributeName, value);
        }

        public static void Attach<TCommand, TTarget, TValue>(this ICommandExecutionContext context, ITypedCommandHandler<TCommand, TTarget> handler, Expression<Func<TTarget, IEnumerable<TValue>>> propertyExpression, ObjectId refereeId)
            where TCommand : TypedCommand<TTarget>
        {
            DoAttach(propertyExpression, context, refereeId);
        }

        public static void Attach<TCommand, TTarget, TValue>(this ICommandExecutionContext context, ITypedCommandHandler<TCommand, TTarget> handler, Expression<Func<TTarget, TValue>> propertyExpression, ObjectId refereeId)
            where TCommand : TypedCommand<TTarget>
        {
            DoAttach(propertyExpression, context, refereeId);
        }

        public static void Detach<TCommand, TTarget, TValue>(this ICommandExecutionContext context, ITypedCommandHandler<TCommand, TTarget> handler, Expression<Func<TTarget, IEnumerable<TValue>>> propertyExpression, ObjectId refereeId)
            where TCommand : TypedCommand<TTarget>
        {
            DoDetach(propertyExpression, context, refereeId);
        }

        public static void Detach<TCommand, TTarget, TValue>(this ICommandExecutionContext context, ITypedCommandHandler<TCommand, TTarget> handler, Expression<Func<TTarget, TValue>> propertyExpression, ObjectId refereeId)
            where TCommand : TypedCommand<TTarget>
        {
            DoDetach(propertyExpression, context, refereeId);
        }

        private static void DoAttach<TTarget, TValue>(Expression<Func<TTarget, TValue>> propertyExpression, ICommandExecutionContext context, ObjectId refereeId)
        {
            context.Attach(refereeId, GetRelationName(propertyExpression));
        }

        private static void DoDetach<TTarget, TValue>(Expression<Func<TTarget, TValue>> propertyExpression, ICommandExecutionContext context, ObjectId refereeId)
        {
            context.Detach(refereeId, GetRelationName(propertyExpression));
        }

        private static string GetRelationName<TTarget, TValue>(Expression<Func<TTarget, TValue>> propertyExpression)
        {
            var propertyInfo = GetPropertyInfo(propertyExpression);
            return AttributeBasedObjectTypeMapping.CreateRelationDescriptor(propertyInfo).RelationName;
        }

        public static  PropertyInfo GetPropertyInfo<TTarget, TValue>(Expression<Func<TTarget, TValue>> propertyExpression)
        {
            Type type = typeof(TTarget);
            var member = propertyExpression.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentException(string.Format("Expression '{0}' refers to a method, not a property.", propertyExpression));                
            }
            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException(string.Format("Expression '{0}' refers to a field, not a property.", propertyExpression));
            }

            if (type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
            {
                throw new ArgumentException(string.Format("Expresion '{0}' refers to a property that is not from type {1}.", propertyExpression, type));
            }
            return propInfo;
        }
    }
}