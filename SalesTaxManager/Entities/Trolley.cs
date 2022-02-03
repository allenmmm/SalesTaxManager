using SalesTaxManager.Configuration;
using System.Collections.Generic;

namespace SalesTaxManager.Entities
{
    public class Trolley
    {
        private List<Item> _Items = new List<Item>();
        public IEnumerable<Item> Items => _Items.AsReadOnly();

        public decimal TrolleyTax { get; private set; }
        public decimal TrolleyTotal { get; private set; }

        public Trolley(IEnumerable<ItemContent> items)
        {
            foreach (var item in items)
            {
                _Items.Add( new Item(item));
            }
        }

        public void CalculateTax(
            TaxRates taxRates,
            ProductsExemptFromSalesTax productsExemptFromSalesTax)
        {

            decimal trolleySalesTax = 0m;
          
            foreach( var item in Items)
            {
                item.PrintPreTax();
               decimal itemTax = item.CalculateSalesTax(
                    taxRates, 
                    productsExemptFromSalesTax);

                itemTax += item.CalculateImportDuty(taxRates.Import);
                trolleySalesTax += itemTax;
                item.SetTax(itemTax);
                TrolleyTotal += item.PriceWithTax;
            }
            TrolleyTax = trolleySalesTax;
        }
        public override string ToString() 
        {
            string s = null;
            foreach( var i in Items)
            {
                s+= i.ToString();
            }
             s += $"Sales Taxes: £{TrolleyTax}\n" +
                $"Total: £{TrolleyTotal}";
            return s;
        }
    }
}
