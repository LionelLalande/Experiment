using System;
using System.Collections.Generic;

namespace FluentValidation
{
    public class DictionaryAssert<K, V> : EnumerableAssert<KeyValuePair<K, V>>, IDictionaryAssert<K, V>
    {
        public new IDictionary<K, V> Value { get; }

        internal DictionaryAssert(IDictionary<K, V> value) : base(value)
        {
            Value = value;
        }

        public IDictionaryAssert<K, V> All(Action<K, V> action)
        {
            throw new NotImplementedException();
        }

        IDictionaryAssert<K, V> IDictionaryAssert<K, V>.Any()
        {
            throw new NotImplementedException();
        }

        public IDictionaryAssert<K, V> Any(Action<K, V> action)
        {
            throw new NotImplementedException();
        }

        public IDictionaryAssert<K, V> ContainsKey(K key)
        {
            throw new NotImplementedException();
        }

        public IDictionaryAssert<K, V> ContainsValue(V value)
        {
            throw new NotImplementedException();
        }

        IDictionaryAssert<K, V> IDictionaryAssert<K, V>.IsEmpty()
        {
            throw new NotImplementedException();
        }

        IDictionaryAssert<K, V> IDictionaryAssert<K, V>.IsNotEmpty()
        {
            throw new NotImplementedException();
        }

        IDictionaryAssert<K, V> IDictionaryAssert<K, V>.HasSingleValue()
        {
            throw new NotImplementedException();
        }
    }
}
