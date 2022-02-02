using SalesTaxManager.Entities;
using SalesTaxManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesTaxManager.Configuration
{
    public class ProductsExemptFromTaxContentConverter : IConverter<ProductsExemptFromTax, ProductsContent>
    {
        public ProductsContent Convert(ProductsExemptFromTax source_object)
        {
            throw new NotImplementedException();
        }

        public ProductsExemptFromTax Convert(ProductsContent source_object)
        {
            return new ProductsExemptFromTax(source_object);
        }
    }
}
