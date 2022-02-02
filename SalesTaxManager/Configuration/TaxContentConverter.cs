using SalesTaxManager.Entities;
using SalesTaxManager.Interfaces;
using SalesTaxManager.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesTaxManager.Configuration
{
    public class TaxContentConverter : IConverter<TaxRates, Codes>
    {
        public Codes Convert(TaxRates source_object)
        {
            throw new NotImplementedException();
        }

        public TaxRates Convert(Codes source_object)
        {
            return new TaxRates(source_object);
        }
    }
}
