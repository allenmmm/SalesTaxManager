using SalesTaxManager.Entities;
using SalesTaxManager.Interfaces;
using System;

namespace SalesTaxManager.Configuration
{
    public class ProductsExemptFromTaxContentConverter : IConverter<ProductsExemptFromSalesTax, ProductsContent>
    {
        public ProductsContent Convert(ProductsExemptFromSalesTax source_object)
        {
            throw new NotImplementedException();
        }

        public ProductsExemptFromSalesTax Convert(ProductsContent source_object)
        {
            return new ProductsExemptFromSalesTax(source_object);
        }
    }
}
