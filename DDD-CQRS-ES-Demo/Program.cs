using System;
using cs = DDD_CQRS_ES_CSharp.ProductContext;
using fs = DDD_CQRS_ES_FSharp.ProductContext;

namespace DDD_CQRS_ES_Demo
{
    internal class Program
    {
        private static void Main()
        {
            InvokeCSharp();
            InvokeFSharp();
        }

        private static void InvokeCSharp()
        {
            var createProduct = new cs.Create { Name = "Product #1" };
            var product = cs.Exec(null)(createProduct);

            var renameProduct = new cs.Rename { Name = "Product #100" };
            product = cs.Exec(product)(renameProduct);
        }

        private static void InvokeFSharp()
        {
            var createProduct = fs.Command.NewCreate("Product #1");
            var product = fs.exec(null).Invoke(createProduct);

            var renameProduct = fs.Command.NewRename("Product #100");
            product = fs.exec(product).Invoke(renameProduct);
        }
    }
}
