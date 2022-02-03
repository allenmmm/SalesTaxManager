using SalesTaxManager.Entities;
using System;

namespace SalesTaxManager.Calculation
{
    public class TaxCalculator
    {
        public decimal TotalSalesTax { get; private set; }
        public TaxCalculator(
            Trolley trolley,
            TaxRates taxRates,
            ProductsExemptFromSalesTax productsExemptFromTax)
        {
            Console.WriteLine("PRE TAX");
            Console.WriteLine("=======");
            trolley.CalculateTax(
                taxRates,
                productsExemptFromTax);
            Console.WriteLine();
            Console.WriteLine("POST TAX");
            Console.WriteLine("=======");
            Console.WriteLine( trolley.ToString());
        }
    }
}
