using SalesTaxManager.Configuration;
using SalesTaxManager.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesTaxManager.Entities
{
    public class Item
    {
        public string Product { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        public bool Imported { get; private set; }
        public decimal PriceWithTax { get; private set; }

        public Item(ItemContent item)
        {
            Guard.AgainstEmpty(item.Product, "Trolley product must not be null or empty");
            Product = Guard.AgainstNull(item.Product, "Trolley product must not be null or empty");
            Price = Guard.AgainstLessThan(0, item.Price, "Price is less than 0");
            Quantity = Guard.AgainstLessThan(0,item.Quantity, "Quantity is less than 0");
            Imported = item.Imported;
        }

        public void SetTax(decimal tax)
        {
            Guard.AgainstLessThan(0, tax, "Item payable tax is less than 0");
            PriceWithTax = Price + tax;
        }

        public decimal CalculateSalesTax(
            TaxRates taxRate,
             ProductsExemptFromSalesTax productsExemptFromSalesTax)
        {
            if (IsNotTaxable(productsExemptFromSalesTax))
                return 0;

            var taxPayable = Math.Ceiling(CalculatePercentage(taxRate.Sales) / taxRate.SalesRoundingFactor)
                     * taxRate.SalesRoundingFactor;

            return taxPayable;
        }

        public decimal CalculateImportDuty(decimal importTax)
        {
            if (!Imported)
                return 0;
            return CalculatePercentage(importTax);
        }


        private bool IsNotTaxable(ProductsExemptFromSalesTax productsExemptFromSalesTax)
        {

            return (FindString(productsExemptFromSalesTax.Books)||
                FindString(productsExemptFromSalesTax.Foods) ||
                 FindString(productsExemptFromSalesTax.Medicines));
        }

        private bool FindString(IEnumerable<string> parameters)
        {
            return (parameters.Where(s => 
                Product.ToLower().IndexOf(s.ToLower()) != -1).Count() > 0);
        }

        public void PrintPreTax()
        {
            var s = $"{Quantity}";
            if (Imported)
                s += " Imported ";
            s += $" {Product} : £{Price}";

            Console.WriteLine(s);
        }

        public override string ToString()
        {
            var s = $"{Quantity}";
            if (Imported)
                s += " Imported ";
            s += $" {Product} : £{PriceWithTax}\n";

            return s;
        }

        private decimal  CalculatePercentage( decimal percentage)
        {
            return Math.Round(((Price * percentage) / 100),2);
        }
    }
}
