using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace FluentValidation
{
    public interface IStringAssert : IFluent, IObjectAssert
    {
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new string Value { get; }

        IStringAssert Equals(string expected);

        IStringAssert Equals(string expected, bool ignorecase = false);

        IStringAssert Contains(string expected);

        IStringAssert Contains(string expected, StringComparison comparison);

        IStringAssert DoesNotContain(string expected);

        IStringAssert DoesNotContain(string expected, StringComparison comparison);

        IStringAssert IsMatch(string pattern);

        IStringAssert IsMatch(Regex regex);

        IStringAssert DoesNotMatch(string pattern);

        IStringAssert DoesNotMatch(Regex regex);

        IStringAssert StartsWith(string expected);

        IStringAssert StartsWith(string expected, StringComparison comparison);

        IStringAssert EndsWith(string expected);

        IStringAssert EndsWith(string expected, StringComparison comparison);
    }
}
