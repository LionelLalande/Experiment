using System.Collections.Generic;

namespace FluentValidation
{
    public interface ISetAssert<T> : IFluent, IEnumerableAssert<T>
    {
        ISetAssert<T> IsProperSubsetOf(ISet<T> expectedSuperset);

        ISetAssert<T> IsProperSupersetOf(ISet<T> expectedSubset);

        ISetAssert<T> IsSubsetOf(ISet<T> expectedSuperset);

        ISetAssert<T> IsSupersetOf(ISet<T> expectedSubset);
    }
}
