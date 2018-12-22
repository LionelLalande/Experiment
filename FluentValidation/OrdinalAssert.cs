using System;

namespace FluentValidation
{
    public class OrdinalAssert<T> : NumericAssert<T>, IOrdinalAssert<T> where T : struct, IComparable<T>
    {
        internal OrdinalAssert(T value) : base(value)
        {
        }
    }
}
