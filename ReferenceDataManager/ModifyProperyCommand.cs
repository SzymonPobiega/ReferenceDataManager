using System;

namespace ReferenceDataManager
{
    public class ModifyProperyCommand : AbstractCommand
    {
        private readonly string propertyName;
        private readonly object value;

        public ModifyProperyCommand(ObjectId targetObjectId, string propertyName, object value)
            : base(targetObjectId)
        {
            this.propertyName = propertyName;
            this.value = value;
        }

        public override void Execute(ICommandExecutionContext context)
        {
            context.ModifyProperty(propertyName, value);
        }
    }
}