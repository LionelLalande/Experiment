using System;

namespace FluentValidation
{
    public class NullableStructAssert<T> : INullableStructAssert<T> where T : struct
    {
        public T? Value { get; }

        internal NullableStructAssert(T? value)
        {
            Value = value;
        }

        public INullableStructAssert<T> HasValue()
        {
            if (!Value.HasValue) throw new Exception(); // TODO
            return this;
        }

        public INullableStructAssert<T> IsEqualTo(T other)
        {
            if (!Value.Equals(other)) throw new UnexpectedValueException<T?>(Value, other);
            return this;
        }

        public INullableStructAssert<T> IsNotEqualTo(T other)
        {
            if (Value.Equals(other)) throw new UnexpectedValueException<T?>(Value, other);
            return this;
        }
    }
}
