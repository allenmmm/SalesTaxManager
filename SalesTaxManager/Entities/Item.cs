using SalesTaxManager.Configuration;
using SalesTaxManager.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesTaxManager.Entities
{
    public class Item
    {
        public string Product { get; private set; }
        public double Price { get; private set; }
        public int Quantity { get; private set; }
        public bool Imported { get; private set; }

        public Item(ItemContent item)
        {
            Guard.AgainstEmpty(item.Product, "Trolley product must not be null or empty");
            Product = Guard.AgainstNull(item.Product, "Trolley product must not be null or empty");
            Price = Guard.AgainstLessThan(0, item.Price, "Price is less than 0");
            Quantity = Guard.AgainstLessThan(0,item.Quantity, "Quantity is less than 0");
            Imported = item.Imported;
        }

    }
}
