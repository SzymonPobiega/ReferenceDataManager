using System;

namespace ReferenceDataManager
{
    public class LocalDateTimeReferenceValueType : IReferenceValueType
    {
        public IComparable GetCurrentValue()
        {
            return DateTime.Now;
        }

        public bool IsCompatible(IComparable value)
        {
            if (!(value is DateTime))
            {
                return false;
            }
            var dateValue = (DateTime) value;
            return dateValue.Kind == DateTimeKind.Local;
        }
    }
}