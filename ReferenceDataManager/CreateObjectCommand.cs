﻿using System;

namespace ReferenceDataManager
{
    public class CreateObjectCommand : AbstractCommand
    {
        private readonly Guid objectTypeId;

        public CreateObjectCommand(Guid objectTypeId, ObjectId objectId)
            : base(objectId)
        {
            this.objectTypeId = objectTypeId;
        }

        public override void Execute(ICommandExecutionContext context)
        {
            context.Create(objectTypeId);
        }
    }
}