using System;
using System.Collections.Generic;

namespace FluentValidation
{
    public class CollectionAssert<T> : EnumerableAssert<T>, ICollectionAssert<T>
    {
        public new ICollection<T> Value { get; }
        
        internal CollectionAssert(ICollection<T> value)
            : base(value)
        {
            Value = value;
        }

        ICollectionAssert<T> ICollectionAssert<T>.All(Action<T> action)
        {
            throw new NotImplementedException();
        }

        ICollectionAssert<T> ICollectionAssert<T>.Any()
        {
            throw new NotImplementedException();
        }

        ICollectionAssert<T> ICollectionAssert<T>.Any(Action<T> action)
        {
            throw new NotImplementedException();
        }

        ICollectionAssert<T> ICollectionAssert<T>.Contains(T item)
        {
            throw new NotImplementedException();
        }

        ICollectionAssert<T> ICollectionAssert<T>.Contains(Predicate<T> filter)
        {
            throw new NotImplementedException();
        }

        ICollectionAssert<T> ICollectionAssert<T>.IsEmpty()
        {
            throw new NotImplementedException();
        }

        ICollectionAssert<T> ICollectionAssert<T>.IsNotEmpty()
        {
            throw new NotImplementedException();
        }

        ICollectionAssert<T> ICollectionAssert<T>.HasSingleValue()
        {
            throw new NotImplementedException();
        }
    }
}
