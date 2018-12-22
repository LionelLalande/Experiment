using System;
using System.Collections.Generic;

namespace FluentValidation
{
    public interface INumericAssert<T> : IFluent, IStructAssert<T> where T : struct, IComparable<T>
    {
        INumericAssert<T> IsInRange(T lower, T upper);

        INumericAssert<T> IsInRange(T lower, T upper, IComparer<T> comparer);

        INumericAssert<T> IsNotInRange(T lower, T upper);

        INumericAssert<T> IsNotInRange(T lower, T upper, IComparer<T> comparer);
    }
}
