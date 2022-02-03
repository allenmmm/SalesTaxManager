using SalesTaxManager.Entities;
using SalesTaxManager.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SalesTaxManager.Configuration
{
    public class ProductsContent
    {
        public IEnumerable<string> Books { get; set; }
        public IEnumerable<string> Foods { get; set; }
        public IEnumerable<string> Medicines { get; set; }
    }

    public class ProductsExemptFromTaxContent : File<ProductsContent>
    {
        public ProductsExemptFromSalesTax ProductsExemptFromTax { get; private set; }
        public ProductsExemptFromTaxContent(
            string fileName,
            IConverter<ProductsExemptFromSalesTax, ProductsContent> converter ) : base(fileName, "ProductsExcemptTax")
        {
            ProductsExemptFromTax = converter.Convert(_Data.First());
        }
    }
}
