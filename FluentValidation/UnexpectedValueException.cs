using System;

namespace FluentValidation
{
    [Serializable]
    public sealed class UnexpectedValueException<T> : Exception
    {
        private readonly T _actual;
        private readonly T _expected;

        public UnexpectedValueException(T actual, T expected)
            : this(actual, expected, CretaeMessage(actual, expected))
        {
        }

        public UnexpectedValueException(T actual, T expected, string message)
            : base(message)
        {
            _actual = actual;
            _expected = expected;
        }

        private static string CretaeMessage(T actual, T expected) =>
            $"Expected value: {expected} instead of {actual}";
    }
}