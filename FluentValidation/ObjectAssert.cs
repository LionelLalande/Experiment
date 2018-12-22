using System;

namespace FluentValidation
{
    public class ObjectAssert : IObjectAssert
    {
        public object Value { get; }

        internal ObjectAssert(object value)
        {
            Value = value;
        }

        public IObjectAssert IsEqualTo(object expected)
        {
            if (!Value.Equals(expected)) throw new UnexpectedValueException<object>(Value, expected);
            return this;
        }

        public IObjectAssert IsNotEqualTo(object expected)
        {
            if (Value.Equals(expected)) throw new UnexpectedValueException<object>(Value, expected);
            return this;
        }

        public IObjectAssert IsSameAs(object expected)
        {
            if (!object.ReferenceEquals(Value, expected)) throw new UnexpectedValueException<object>(Value, expected);
            return this;
        }

        public IObjectAssert IsNotSameAs(object expected)
        {
            if (object.ReferenceEquals(Value, expected)) throw new UnexpectedValueException<object>(Value, expected);
            return this;
        }

        public IObjectAssert IsNull()
        {
            if (!(Value is null)) throw new UnexpectedValueException<object>(Value, null);
            return this;
        }

        public IObjectAssert IsNotNull()
        {
            if (Value is null) throw new UnexpectedValueException<object>(Value, null);
            return this;
        }

        public ITypeAssert IsAssignableFrom<T>()
        {
            throw new NotImplementedException();
        }

        public ITypeAssert IsAssignableFrom(Type type)
        {
            throw new NotImplementedException();
        }

        public ITypeAssert IsType<T>()
        {
            throw new NotImplementedException();
        }

        public ITypeAssert IsType(Type type)
        {
            throw new NotImplementedException();
        }

        public ITypeAssert IsNotType<T>()
        {
            throw new NotImplementedException();
        }

        public ITypeAssert IsNotType(Type type)
        {
            throw new NotImplementedException();
        }
    }
}
