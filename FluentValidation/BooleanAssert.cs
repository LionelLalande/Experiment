namespace FluentValidation
{
    public class BooleanAssert : StructAssert<bool>, IBooleanAssert
    {
        internal BooleanAssert(bool value)
            : base(value)
        {
        }

        public IBooleanAssert IsFalse()
        {
            if (!Value) throw new UnexpectedValueException<bool>(Value, false);
            return this;
        }

        public IBooleanAssert IsTrue()
        {
            if (Value) throw new UnexpectedValueException<bool>(Value, true);
            return this;
        }
    }
}
