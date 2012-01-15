using System;

namespace ReferenceDataManager
{
    public struct ObjectId : IEquatable<ObjectId>
    {
        private readonly Guid uniqueId;

        public ObjectId(Guid uniqueId)
        {
            this.uniqueId = uniqueId;
        }

        public override string ToString()
        {
            return uniqueId.ToString();
        }

        public static ObjectId NewUniqueId()
        {
            return new ObjectId(Guid.NewGuid());
        }

        public bool Equals(ObjectId other)
        {
            return other.uniqueId.Equals(uniqueId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != typeof (ObjectId)) return false;
            return Equals((ObjectId) obj);
        }

        public override int GetHashCode()
        {
            return uniqueId.GetHashCode();
        }

        public static bool operator ==(ObjectId left, ObjectId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ObjectId left, ObjectId right)
        {
            return !left.Equals(right);
        }
    }
}