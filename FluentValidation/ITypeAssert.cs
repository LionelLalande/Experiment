using System;

namespace FluentValidation
{
    public interface ITypeAssert : IFluent
    {
        ITypeAssert IsAssignableFrom<T>();

        ITypeAssert IsAssignableFrom(Type type);

        ITypeAssert IsType<T>();

        ITypeAssert IsType(Type type);

        ITypeAssert IsNotType<T>();

        ITypeAssert IsNotType(Type type);
    }
}
