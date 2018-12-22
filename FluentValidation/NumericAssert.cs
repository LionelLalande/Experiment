using System;
using System.Collections.Generic;

namespace FluentValidation
{
    public class NumericAssert<T> : StructAssert<T>, INumericAssert<T> where T : struct, IComparable<T>
    {
        internal NumericAssert(T value) : base(value) { }

        public INumericAssert<T> IsInRange(T lower, T upper)
        {
            if (Value.CompareTo(lower) < 0 || Value.CompareTo(upper) > 0) throw new /*OutOfRange*/Exception(); // TODO
            return this;
        }

        public INumericAssert<T> IsInRange(T lower, T upper, IComparer<T> comparer)
        {
            if (comparer.Compare(Value, lower) < 0 || comparer.Compare(Value, upper) > 0) throw new /*OutOfRange*/Exception(); // TODO
            return this;
        }

        public INumericAssert<T> IsNotInRange(T lower, T upper)
        {
            if (Value.CompareTo(lower) > 0 && Value.CompareTo(upper) < 0) throw new Exception(); // TODO
            return this;
        }

        public INumericAssert<T> IsNotInRange(T lower, T upper, IComparer<T> comparer)
        {
            if (comparer.Compare(Value, lower) > 0 && comparer.Compare(Value, upper) < 0) throw new Exception(); // TODO
            return this;
        }
    }
}
