using System.ComponentModel;

namespace FluentValidation
{
    public interface IObjectAssert : IFluent, ITypeAssert
    {
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        object Value { get; }

        IObjectAssert IsEqualTo(object expected);

        IObjectAssert IsNotEqualTo(object expected);

        IObjectAssert IsSameAs(object expected);

        IObjectAssert IsNotSameAs(object expected);

        IObjectAssert IsNull();

        IObjectAssert IsNotNull();
    }
}
