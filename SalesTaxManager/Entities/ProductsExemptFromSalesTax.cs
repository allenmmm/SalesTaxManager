using SalesTaxManager.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace SalesTaxManager.Entities
{
    public class ProductsExemptFromSalesTax
    {
        public IEnumerable<string> Books { get; private set; }
        public IEnumerable<string> Foods { get; private set; }
        public IEnumerable<string> Medicines { get; private set; }

        public ProductsExemptFromSalesTax(ProductsContent products )
        {
            Books = products.Books.ToList();
            Foods = products.Foods.ToList();
            Medicines = products.Medicines.ToList();
        }
    }
}
