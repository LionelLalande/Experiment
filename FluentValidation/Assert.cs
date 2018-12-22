using System.Collections.Generic;

namespace FluentValidation
{
    public static class Check
    {
        public static IBooleanAssert That(bool value) => new BooleanAssert(value);

        public static IOrdinalAssert<byte> That(byte value) => new OrdinalAssert<byte>(value);

        public static IOrdinalAssert<sbyte> That(sbyte value) => new OrdinalAssert<sbyte>(value);

        public static IOrdinalAssert<ushort> That(ushort value) => new OrdinalAssert<ushort>(value);

        public static IOrdinalAssert<short> That(short value) => new OrdinalAssert<short>(value);

        public static IOrdinalAssert<uint> That(uint value) => new OrdinalAssert<uint>(value);

        public static IOrdinalAssert<int> That(int value) => new OrdinalAssert<int>(value);

        public static IOrdinalAssert<ulong> That(ulong value) => new OrdinalAssert<ulong>(value);

        public static IOrdinalAssert<long> That(long value) => new OrdinalAssert<long>(value);

        public static INumericAssert<float> That(float value) => new NumericAssert<float>(value);

        public static INumericAssert<double> That(double value) => new NumericAssert<double>(value);

        public static INumericAssert<decimal> That(decimal value) => new NumericAssert<decimal>(value);

        public static IOrdinalAssert<char> That(char value) => new OrdinalAssert<char>(value);

        public static IStringAssert That(string value) => new StringAssert(value);

        public static IStructAssert<T> That<T>(T value) where T : struct => new StructAssert<T>(value);

        public static INullableStructAssert<T> That<T>(T? value) where T : struct => new NullableStructAssert<T>(value);

        public static IObjectAssert That(object value) => new ObjectAssert(value);

        public static IEnumerableAssert<T> That<T>(IEnumerable<T> value) => new EnumerableAssert<T>(value);

        public static ICollectionAssert<T> That<T>(ICollection<T> value) => new CollectionAssert<T>(value);

        public static IDictionaryAssert<K, V> That<K, V>(IDictionary<K, V> value) => new DictionaryAssert<K, V>(value);

        public static ISetAssert<T> That<T>(ISet<T> value) => new SetAssert<T>(value);
    }
}
