using System.ComponentModel;

namespace FluentValidation
{
    public interface IStructAssert<T> : IFluent where T : struct
    {
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        T Value { get; }

        IStructAssert<T> IsDefaultValue();

        IStructAssert<T> IsNotDefaultValue();

        IStructAssert<T> IsEqualTo(T other);

        IStructAssert<T> IsNotEqualTo(T other);
    }
}
