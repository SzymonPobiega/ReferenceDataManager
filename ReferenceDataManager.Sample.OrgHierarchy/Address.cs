using System;

namespace ReferenceDataManager.Sample.OrgHierarchy
{
    public class Address : IEquatable<Address>
    {
        private readonly string street;
        private readonly string houseNumber;
        private readonly string city;
        private readonly string contryCode;

        public Address(string street, string houseNumber, string city, string contryCode)
        {
            this.street = street;
            this.contryCode = contryCode;
            this.city = city;
            this.houseNumber = houseNumber;
        }

        public string ContryCode
        {
            get { return contryCode; }
        }

        public string City
        {
            get { return city; }
        }

        public string HouseNumber
        {
            get { return houseNumber; }
        }

        public string Street
        {
            get { return street; }
        }

        public bool Equals(Address other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.street, street) && Equals(other.houseNumber, houseNumber) && Equals(other.city, city) && Equals(other.contryCode, contryCode);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Address)) return false;
            return Equals((Address) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (street != null ? street.GetHashCode() : 0);
                result = (result*397) ^ (houseNumber != null ? houseNumber.GetHashCode() : 0);
                result = (result*397) ^ (city != null ? city.GetHashCode() : 0);
                result = (result*397) ^ (contryCode != null ? contryCode.GetHashCode() : 0);
                return result;
            }
        }

        public static bool operator ==(Address left, Address right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Address left, Address right)
        {
            return !Equals(left, right);
        }
    }
}