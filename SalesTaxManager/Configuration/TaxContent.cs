using SalesTaxManager.Entities;
using SalesTaxManager.Interfaces;
using System.Linq;

namespace SalesTaxManager.Configuration
{
    public class Codes
    {
        public decimal Sales { get; set; }
        public decimal Import { get; set; }
        public decimal SalesRoundingFactor { get; set; }
    }

    public class TaxContent: File<Codes>
    {
        public TaxRates TaxRates { get; private set; }
        public TaxContent(
            string fileName,
             IConverter<TaxRates, Codes> converter) : base(fileName, "TaxParameters")
        {
            TaxRates = converter.Convert(_Data.First());
        }
    }
}
