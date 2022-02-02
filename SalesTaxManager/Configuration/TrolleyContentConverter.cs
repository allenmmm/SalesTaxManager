using SalesTaxManager.Entities;
using SalesTaxManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesTaxManager.Configuration
{
    public class TrolleyContentConverter : IConverterList<Trolley, ItemContent>
    {
        public Trolley Convert(IEnumerable<ItemContent> source_object)
        {
            return new Trolley(source_object);
        }

        public IEnumerable<ItemContent> Convert(Trolley source_object)
        {
            throw new NotImplementedException();
        }
    }
}
