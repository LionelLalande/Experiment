using System;

namespace FluentValidation
{
    public class StructAssert<T> : IStructAssert<T> where T : struct
    {
        public T Value { get; }

        internal StructAssert(T value)
        {
            Value = value;
        }

        public IStructAssert<T> IsDefaultValue()
        {
            if (!Value.Equals(default(T))) throw new UnexpectedValueException<T>(Value, default);
            return this;
        }

        public IStructAssert<T> IsNotDefaultValue()
        {
            if (Value.Equals(default(T))) throw new Exception(); // TODO
            return this;
        }

        public IStructAssert<T> IsEqualTo(T other)
        {
            if (!Value.Equals(other)) throw new UnexpectedValueException<T>(Value, other);
            return this;
        }

        public IStructAssert<T> IsNotEqualTo(T other)
        {
            if (!Value.Equals(default(T))) throw new UnexpectedValueException<T>(Value, other);
            return this;
        }
    }
}
