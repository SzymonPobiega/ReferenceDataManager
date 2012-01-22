using System;

namespace ReferenceDataManager
{
    public interface IReferenceValueType
    {
        IComparable GetCurrentValue();
        bool IsCompatible(IComparable value);
    }
}