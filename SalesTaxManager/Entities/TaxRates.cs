using SalesTaxManager.Configuration;
using SalesTaxManager.Utils;

namespace SalesTaxManager.Entities
{
    public class TaxRates
    {
        public decimal Sales { get; private set; }
        public decimal Import { get; private set; }
        public decimal SalesRoundingFactor { get; private set; }

        public TaxRates(Codes codes)
        {
            Sales = Guard.AgainstLessThan(0, codes.Sales, "Basic rate is less than 0");
            Import = Guard.AgainstLessThan(0, codes.Import, "Import tax rate is less than 0");
            SalesRoundingFactor = Guard.AgainstLessThan(0, codes.SalesRoundingFactor, "Sales tax rate is less than 0");
        }
    }
}
