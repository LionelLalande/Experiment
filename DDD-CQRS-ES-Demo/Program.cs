using DDD_CQRS_ES_CSharp;
using FluentValidation;

using cs = DDD_CQRS_ES_CSharp.ProductContext;
using fs = DDD_CQRS_ES_FSharp.ProductContext;
using static DDD_CQRS_ES_CSharp.ProductContext;

namespace DDD_CQRS_ES_Demo
{
    internal static class Program
    {
        private static void Main()
        {
            InvokeCSharp();
            InvokeFSharp();
        }

        private static void InvokeCSharp()
        {
            // Setup context
            var store = new InMemoryEventStore<Id, Event>();
            var context = new ProductContext(store);
            var handler = new CommandHandler<Id, Product, Command, cs.Event>(context);

            // Create a product
            var createProduct = new Command<Id, Create>(default, new Create { Name = "Product #1" });
            var ar = handler.Apply(createProduct);

            // Checks
            Check.That(ar).IsNotNull();
            Check.That(ar.Id).IsNotDefaultValue();
            Check.That(ar.Value.Name).IsEqualTo("Product #1");

            // Rename a product
            var renameProduct = new Command<Id, Rename>(ar.Id, new Rename { Name = "Product #100" });
            ar = handler.Apply(renameProduct);

            // Checks
            Check.That(ar.Value.Name).IsEqualTo("Product #100");
        }

        private static void InvokeFSharp()
        {
            // Create a product
            var createProduct = fs.Command.NewCreate("Product #1");
            var product = fs.exec(null).Invoke(createProduct);

            // Checks
            Check.That(product).IsNotNull();
            Check.That(product.Id).IsNotDefaultValue();
            Check.That(product.Name).IsEqualTo("Product #1");

            // Rename a product
            var renameProduct = fs.Command.NewRename("Product #100");
            product = fs.exec(product).Invoke(renameProduct);

            // Checks
            Check.That(product.Name).IsEqualTo("Product #100");
        }
    }
}
