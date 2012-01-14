using System;

namespace ReferenceDataManager
{
    public struct ObjectTypeId : IEquatable<ObjectTypeId>
    {
        private readonly Guid uniqueId;

        public ObjectTypeId(Guid uniqueId)
        {
            this.uniqueId = uniqueId;
        }

        public bool Equals(ObjectTypeId other)
        {
            return other.uniqueId.Equals(uniqueId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != typeof (ObjectTypeId)) return false;
            return Equals((ObjectTypeId) obj);
        }

        public override int GetHashCode()
        {
            return uniqueId.GetHashCode();
        }

        public static bool operator ==(ObjectTypeId left, ObjectTypeId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ObjectTypeId left, ObjectTypeId right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return uniqueId.ToString();
        }

        public static ObjectTypeId NewUniqueId()
        {
            return new ObjectTypeId(Guid.NewGuid());
        }
    }
}