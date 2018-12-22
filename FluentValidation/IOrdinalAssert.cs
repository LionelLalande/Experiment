using System;

namespace FluentValidation
{
    public interface IOrdinalAssert<T> : IFluent, INumericAssert<T> where T : struct, IComparable<T>
    {
    }
}
