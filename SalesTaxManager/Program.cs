using SalesTaxManager.Calculation;
using SalesTaxManager.Configuration;

namespace SalesTaxManager
{
    class Program
    {
        static void Main(string[] args)
        {
            var productsSource = new ProductsExemptFromTaxContent(
                @"TestData\ProductsExemptFromTax.json",
                new ProductsExemptFromTaxContentConverter());

            var trolleySource = new TrolleyContent(
                 @"TestData\Trolley.json",
                 new TrolleyContentConverter());

            var taxSource = new TaxContent(
                   @"TestData\Tax.json",
                   new TaxContentConverter());

            var taxCalc = new TaxCalculator(
                trolleySource.Trolley,
                taxSource.TaxRates,
                productsSource.ProductsExemptFromTax);

            taxCalc.ToString();
        }
    }
}
