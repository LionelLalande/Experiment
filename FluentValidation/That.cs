//using System;
//using System.Collections.Generic;

//namespace FluentValidation
//{
//    public interface IThat<T>
//    {
//        IEquatableThat<T> IsEqualTo(T other);
//        IEquatableThat<T> IsEqualTo(T other, IEqualityComparer<T> comparer);
//        IEquatableThat<T> IsEqualTo(T other, IComparer<T> comparer);
//        IEquatableThat<T> IsEqualTo(T other, Comparison<T> comparison);
//    }
//    public interface IEquatableThat<T>
//    {
//        IEquatableThat<T> AndIsEqualTo(T other);
//        IEquatableThat<T> AndIsEqualTo(T other, IEqualityComparer<T> comparer);
//        IEquatableThat<T> AndIsEqualTo(T other, IComparer<T> comparer);
//        IEquatableThat<T> AndIsEqualTo(T other, Comparison<T> comparison);
//    }
//    public interface IStructThat<T> where T : struct, IComparable<T>, IThat<T>
//    {
//        void IsInRange(T lower, T upper, bool inclusive = true);
//    }
//    public interface INullableThat<T> where T : class
//    {
//        IAndThat IsNull();
//        IAndThat IsNotNull();
//    }
//    public interface IAndThat
//    {
//        IThat<T> And<T>(T value);
//    }



//    public sealed class NullableThat<T> : INullableThat<T>
//    {
//        internal NullableThat(T value)
//        {
//            _value = value;
//        }

//        public IAndThat IsNull()
//        {
//            if (_value != null) throw new AssertException();
//            return new AndThat();
//        }

//        public IAndThat IsNotNull()
//        {
//            if (_value == null) throw new AssertException();
//            return new AndThat();
//        }

//        private readonly T _value;
//    }
//    internal sealed class AndThat : IAndThat
//    {
//        public 
//    }
//}
