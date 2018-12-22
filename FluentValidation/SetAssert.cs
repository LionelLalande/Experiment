using System;
using System.Collections.Generic;

namespace FluentValidation
{
    public class SetAssert<T> : CollectionAssert<T>, ISetAssert<T>
    {
        public new ISet<T> Value { get; }

        internal SetAssert(ISet<T> value) : base(value)
        {
            Value = value;
        }

        public ISetAssert<T> IsProperSubsetOf(ISet<T> expectedSuperset)
        {
            if (!Value.IsProperSupersetOf(expectedSuperset)) throw new Exception(); // TODO
            return this;
        }

        public ISetAssert<T> IsProperSupersetOf(ISet<T> expectedSubset)
        {
            if (!Value.IsProperSubsetOf(expectedSubset)) throw new Exception(); // TODO
            return this;
        }

        public ISetAssert<T> IsSubsetOf(ISet<T> expectedSuperset)
        {
            if (!Value.IsSupersetOf(expectedSuperset)) throw new Exception(); // TODO
            return this;
        }

        public ISetAssert<T> IsSupersetOf(ISet<T> expectedSubset)
        {
            if (!Value.IsSubsetOf(expectedSubset)) throw new Exception(); // TODO
            return this;
        }
    }
}
