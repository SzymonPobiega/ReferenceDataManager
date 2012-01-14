﻿using System;

namespace ReferenceDataManager
{
    public interface ICommandExecutionContext
    {
        void Create(Guid objectTypeId);
        void Attach(ObjectId refereeObjectId, string relationName);
        void ModifyAttribute(string attributeName, object value);
    }
}