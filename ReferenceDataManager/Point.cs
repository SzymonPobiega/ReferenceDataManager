using System;

namespace ReferenceDataManager
{
    public class Point<TReference>
        where TReference : IComparable<TReference>
    {
        private readonly ChangeSetId changeSetId;
        private readonly TReference referenceValue;

        public Point(ChangeSetId changeSetId, TReference referenceValue)
        {
            this.changeSetId = changeSetId;
            this.referenceValue = referenceValue;
        }

        public bool IsValidFor(TReference actualValue)
        {
            return actualValue.CompareTo(referenceValue) >= 0;
        }

        public ChangeSetId ChangeSetId
        {
            get { return changeSetId; }
        }
    }
}