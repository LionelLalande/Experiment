using System;

namespace DDD_CQRS_ES_CSharp
{
    public class ProductContext : IContext<ProductContext.Id, ProductContext.Product, ProductContext.Command, ProductContext.Event>
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
            ////public Id Id { get; set; }

            public string Name { get; set; }
        }

        public class Renamed : Event
        {
            public string Name { get; set; }
        }

        public sealed class Product // : Entity<Product>, IEntity
        {
            ////public Id Id { get; private set; }

            public string Name { get; private set; }

            public void Apply(Created command)
            {
                ////Id = command.Id;
                Name = command.Name;
            }

            public void Apply(Renamed command)
            {
                Name = command.Name;
            }
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

        // ==================================

        public Func<Command, Event> Exec(Product entity) =>
            (command) =>
            {
                var @event = ToEvent(command);
                //entity.Apply((dynamic)@event);
                return @event;
            };

        ////public Product ApplyEvent<TEvent>(Product p, TEvent e)
        ////{
        ////    p.Apply((dynamic)e);
        ////    return p;
        ////}

        // ==================================

        public IRepository<Id, Product> Repository { get; }

        public ProductContext(IEventStore<Id, Event> store)
        {
            Repository = new EventStoreRepository<Id, Product, Command, Event>(store, this);
        }

        public Product CreateNew()
        {
            return new Product();
        }

        public Id CreateNewId()
        {
            return new Id(Guid.NewGuid());
        }

        public Product ApplyEvent(Product product, Event @event)
        {
            product.Apply((dynamic)@event);
            return product;
        }

        private static Event ToEvent(Command command)
        {
            switch (command)
            {
                ////case Create create: return new Created { Id = new Id(Guid.NewGuid()), Name = Assert.IsValidName(create.Name) };
                case Create create: return new Created { Name = Assert.IsValidName(create.Name) };
                case Rename rename: return new Renamed { Name = Assert.IsValidName(rename.Name) };
                default: throw new NotSupportedException();
            }
        }
    }
}
