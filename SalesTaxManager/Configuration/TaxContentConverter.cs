using SalesTaxManager.Entities;
using SalesTaxManager.Interfaces;
using System;

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
