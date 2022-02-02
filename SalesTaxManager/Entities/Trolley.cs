using SalesTaxManager.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesTaxManager.Entities
{
    public class Trolley
    {
        private List<Item> _Items = new List<Item>();
        public IEnumerable<Item> Items => _Items.AsReadOnly();

        public Trolley(IEnumerable<ItemContent> items)
        {
            foreach (var item in items)
            {
                _Items.Add( new Item(item));
            }
        }
    }
}
