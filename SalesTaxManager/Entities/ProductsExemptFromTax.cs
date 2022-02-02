using SalesTaxManager.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalesTaxManager.Entities
{
    public class ProductsExemptFromTax
    {
        public IEnumerable<string> Books { get; private set; }
        public IEnumerable<string> Foods { get; private set; }
        public IEnumerable<string> Medicines { get; private set; }

        public ProductsExemptFromTax(ProductsContent products )
        {
            Books = products.Books.ToList();
            Foods = products.Foods.ToList();
            Medicines = products.Medicines.ToList();
        }
    }
}
