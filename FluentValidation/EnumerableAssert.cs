using System;
using System.Collections.Generic;

namespace FluentValidation
{
    public class EnumerableAssert<T> : ObjectAssert, IEnumerableAssert<T>
    {
        public new IEnumerable<T> Value { get; }

        object IObjectAssert.Value => Value;

        internal EnumerableAssert(IEnumerable<T> value) : base(value)
        {
            Value = value;
        }

        public IEnumerableAssert<T> All(Action<T> action)
        {
            throw new NotImplementedException();
        }

        public IEnumerableAssert<T> Any()
        {
            throw new NotImplementedException();
        }

        public IEnumerableAssert<T> Any(Action<T> action)
        {
            throw new NotImplementedException();
        }

        public IEnumerableAssert<T> Contains(T item)
        {
            throw new NotImplementedException();
        }

        public IEnumerableAssert<T> Contains(Predicate<T> filter)
        {
            throw new NotImplementedException();
        }

        public IEnumerableAssert<T> IsEmpty()
        {
            throw new NotImplementedException();
        }

        public IEnumerableAssert<T> IsNotEmpty()
        {
            throw new NotImplementedException();
        }

        public IEnumerableAssert<T> IsSequenceEqualsTo(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public IEnumerableAssert<T> IsSequenceEqualsTo(IEnumerable<T> other, IEqualityComparer<T> comparer)
        {
            throw new NotImplementedException();
        }

        public IEnumerableAssert<T> HasSingleValue()
        {
            throw new NotImplementedException();
        }
    }
}
