using System;

namespace ReferenceDataManager
{
    public class ModifyAttributeCommand : AbstractCommand
    {
        private readonly string attributeName;
        private readonly object value;

        public ModifyAttributeCommand(ObjectId targetObjectId, string attributeName, object value)
            : base(targetObjectId)
        {
            this.attributeName = attributeName;
            this.value = value;
        }

        public override void Execute(ICommandExecutionContext context)
        {
            context.ModifyAttribute(attributeName, value);
        }
    }
}