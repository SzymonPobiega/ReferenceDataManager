using System;

namespace ReferenceDataManager
{
    public class Point
    {
        private readonly ChangeSetId changeSetId;
        private readonly IComparable referenceValue;

        public Point(ChangeSetId changeSetId, IComparable referenceValue)
        {
            this.changeSetId = changeSetId;
            this.referenceValue = referenceValue;
        }

        public bool IsValidFor(IComparable actualValue)
        {
            return actualValue.CompareTo(ReferenceValue) >= 0;
        }

        public ChangeSetId ChangeSetId
        {
            get { return changeSetId; }
        }

        public IComparable ReferenceValue
        {
            get { return referenceValue; }
        }
    }
}