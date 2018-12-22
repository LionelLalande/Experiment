using System.ComponentModel;

namespace FluentValidation
{
    public interface IFluent
    {
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        bool Equals(object obj);

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        int GetHashCode();

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        string ToString();
    }
}
