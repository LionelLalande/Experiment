using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace FluentValidation
{
    public interface IEnumerableAssert<T> : IFluent, IObjectAssert
    {
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new IEnumerable<T> Value { get; }

        IEnumerableAssert<T> All(Action<T> action);

        IEnumerableAssert<T> Any();

        IEnumerableAssert<T> Any(Action<T> action);

        IEnumerableAssert<T> Contains(T item);

        IEnumerableAssert<T> Contains(Predicate<T> filter);

        IEnumerableAssert<T> IsEmpty();

        IEnumerableAssert<T> IsNotEmpty();

        IEnumerableAssert<T> IsSequenceEqualsTo(IEnumerable<T> other);

        IEnumerableAssert<T> IsSequenceEqualsTo(IEnumerable<T> other, IEqualityComparer<T> comparer);

        IEnumerableAssert<T> HasSingleValue();
    }
}
