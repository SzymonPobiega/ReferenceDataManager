using System;

namespace ReferenceDataManager
{
    public class ObjectId : IEquatable<ObjectId>
    {
        private readonly Guid uniqueId;

        public ObjectId(Guid uniqueId)
        {
            this.uniqueId = uniqueId;
        }

        public bool Equals(ObjectId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.uniqueId.Equals(uniqueId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (ObjectId)) return false;
            return Equals((ObjectId) obj);
        }

        public override int GetHashCode()
        {
            return uniqueId.GetHashCode();
        }

        public static bool operator ==(ObjectId left, ObjectId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ObjectId left, ObjectId right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return uniqueId.ToString();
        }

        public static ObjectId NewUniqueId()
        {
            return new ObjectId(Guid.NewGuid());
        }
    }
}