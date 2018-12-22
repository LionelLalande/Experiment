using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace FluentValidation
{
    public interface ICollectionAssert<T> : IFluent, IEnumerableAssert<T>
    {
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new ICollection<T> Value { get; }

        ICollectionAssert<T> All(Action<T> action);

        ICollectionAssert<T> Any();

        ICollectionAssert<T> Any(Action<T> action);

        ICollectionAssert<T> Contains(T item);

        ICollectionAssert<T> Contains(Predicate<T> filter);

        ICollectionAssert<T> IsEmpty();

        ICollectionAssert<T> IsNotEmpty();

        ICollectionAssert<T> HasSingleValue();
    }
}
