using System;

namespace ReferenceDataManager
{
    public struct ChangeSetId : IEquatable<ChangeSetId>
    {
        private readonly Guid uniqueId;

        public ChangeSetId(Guid uniqueId)
        {
            this.uniqueId = uniqueId;
        }
        
        public override string ToString()
        {
            return uniqueId.ToString();
        }

        public bool Equals(ChangeSetId other)
        {
            return other.uniqueId.Equals(uniqueId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != typeof (ChangeSetId)) return false;
            return Equals((ChangeSetId) obj);
        }

        public override int GetHashCode()
        {
            return uniqueId.GetHashCode();
        }

        public static bool operator ==(ChangeSetId left, ChangeSetId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ChangeSetId left, ChangeSetId right)
        {
            return !left.Equals(right);
        }

        public static ChangeSetId NewUniqueId()
        {
            return new ChangeSetId(Guid.NewGuid());
        }

        public static ChangeSetId Parse(string changeSetId)
        {
            return new ChangeSetId(new Guid(changeSetId));
        }
    }
}