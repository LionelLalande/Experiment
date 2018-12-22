using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace FluentValidation
{
    public interface IDictionaryAssert<K, V> : IFluent, IEnumerableAssert<KeyValuePair<K, V>>
    {
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        new IDictionary<K, V> Value { get; }

        IDictionaryAssert<K, V> All(Action<K, V> action);

        new IDictionaryAssert<K, V> Any();

        IDictionaryAssert<K, V> Any(Action<K, V> action);

        IDictionaryAssert<K, V> ContainsKey(K key);

        IDictionaryAssert<K, V> ContainsValue(V value);

        IDictionaryAssert<K, V> IsEmpty();

        IDictionaryAssert<K, V> IsNotEmpty();

        IDictionaryAssert<K, V> HasSingleValue();
    }
}
