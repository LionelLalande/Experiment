namespace FluentValidation
{
    public interface IBooleanAssert : IFluent, IStructAssert<bool>
    {
        IBooleanAssert IsFalse();

        IBooleanAssert IsTrue();
    }
}
