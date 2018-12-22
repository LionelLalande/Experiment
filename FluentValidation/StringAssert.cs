using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FluentValidation
{
    public class StringAssert : ObjectAssert, IStringAssert
    {
        public new string Value { get; }

        object IObjectAssert.Value => Value;

        internal StringAssert(string value) : base(value)
        {
            Value = value;
        }

        public IStringAssert Equals(string expected)
        {
            if (!string.Equals(Value, expected)) throw new UnexpectedValueException<string>(Value, expected);
            return this;
        }

        public IStringAssert Equals(string expected, bool ignoreCase = false)
        {
            var comparison = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            if (!string.Equals(Value, expected, comparison)) throw new UnexpectedValueException<string>(Value, expected);
            return this;
        }

        public IStringAssert Contains(string expected)
        {
            if (!Value.Contains(expected)) throw new Exception(); // TODO
            return this;
        }

        public IStringAssert Contains(string expected, StringComparison comparison)
        {
            if (Value.IndexOf(expected, comparison) == -1) throw new Exception(); // TODO
            return this;
        }

        public IStringAssert DoesNotContain(string expected)
        {
            if (Value.Contains(expected)) throw new Exception(); // TODO
            return this;
        }

        public IStringAssert DoesNotContain(string expected, StringComparison comparison)
        {
            if (Value.IndexOf(expected, comparison) >= 0) throw new Exception(); // TODO
            return this;
        }

        public IStringAssert IsMatch(string pattern)
        {
            if (!Regex.IsMatch(Value, pattern)) throw new Exception(); // TODO
            return this;
        }

        public IStringAssert IsMatch(Regex regex)
        {
            if (!regex.IsMatch(Value)) throw new Exception(); // TODO
            return this;
        }

        public IStringAssert DoesNotMatch(string pattern)
        {
            if (Regex.IsMatch(Value, pattern)) throw new Exception(); // TODO
            return this;
        }

        public IStringAssert DoesNotMatch(Regex regex)
        {
            if (regex.IsMatch(Value)) throw new Exception(); // TODO
            return this;
        }

        public IStringAssert StartsWith(string expected)
        {
            if (!Value.StartsWith(expected)) throw new Exception(); // TODO
            return this;
        }

        public IStringAssert StartsWith(string expected, StringComparison comparison)
        {
            if (!Value.StartsWith(expected, comparison)) throw new Exception(); // TODO
            return this;
        }

        public IStringAssert EndsWith(string expected)
        {
            if (!Value.EndsWith(expected)) throw new Exception(); // TODO
            return this;
        }

        public IStringAssert EndsWith(string expected, StringComparison comparison)
        {
            if (!Value.EndsWith(expected, comparison)) throw new Exception(); // TODO
            return this;
        }
    }
}
