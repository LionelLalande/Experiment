using System.ComponentModel;

namespace FluentValidation
{
    public interface INullableStructAssert<T> : IFluent where T : struct
    {
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        T? Value { get; }

        INullableStructAssert<T> HasValue();

        INullableStructAssert<T> IsEqualTo(T other);

        INullableStructAssert<T> IsNotEqualTo(T other);
    }
}
