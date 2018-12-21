using System;

namespace DDD_CQRS_ES_CSharp
{
    public static class ProductContext
    {
        // In C# 8.0 transform Id to (see next line) =>
        // public struct Id(Guid value);
        public struct Id : IEquatable<Id>
        {
            public Id(Guid value)
            {
                _value = value;
            }

            public bool Equals(Id other) => _value == other._value;

            public override bool Equals(object obj) => obj is Id other && Equals(other);

            public override int GetHashCode() => _value.GetHashCode();

            public override string ToString() => _value.ToString("N");

            private readonly Guid _value;
        }

        public interface Command { }

        public class Create : Command
        {
            public string Name { get; set; }
        }

        public class Rename : Command
        {
            public string Name { get; set; }
        }

        public interface Event { }

        public class Created : Event
        {
            public Id Id { get; set; }

            public string Name { get; set; }
        }

        public class Renamed : Event
        {
            public string Name { get; set; }
        }

        public sealed class Product // : Entity<Product>, IEntity
        {
            public Id Id { get; set; }

            public string Name { get; set; }

            public void Apply(Created command)
            {
                _name = command.Name;
            }

            public void Apply(Renamed command)
            {
                _name = command.Name;
            }

            private string _name;
        }

        public static Func<Command, Product> Exec(Product product) =>
            (cmd) =>
            {
                switch (cmd)
                {
                    case Create create: return ApplyCore(product, new Created { Id = new Id(Guid.NewGuid()), Name = Assert.IsValidName(create.Name) });
                    case Rename rename: return ApplyCore(product, new Renamed { Name = Assert.IsValidName(rename.Name) });
                    default: throw new NotSupportedException();
                }
            };

        private static Product ApplyCore<TEvent>(Product p, TEvent e)
        {
            p.Apply((dynamic)e);
            return p;
        }

        private static class Assert
        {
            internal static string IsValidName(string name)
            {
                if (string.IsNullOrEmpty(name))
                    throw new ArgumentException(nameof(name), "The name must not be null.");
                return name;
            }
        }
    }
}
