using System;

namespace FluentValidation
{
    public interface IFloatingNumberAssert<T> : IFluent, INumericAssert<T> where T : struct, IComparable<T>
    {
        IFloatingNumberAssert<T> IsInfinity();

        IFloatingNumberAssert<T> IsPositiveInfinity();

        IFloatingNumberAssert<T> IsNegativeInfinity();

        IFloatingNumberAssert<T> IsNaN();
    }
}
