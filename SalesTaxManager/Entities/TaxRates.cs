using SalesTaxManager.Configuration;
using SalesTaxManager.Utils;

namespace SalesTaxManager.Entities
{
    public class TaxRates
    {
        public double Basic { get; private set; }
        public double Import { get; private set; }
        public double Sales{ get; private set; }

        public TaxRates(Codes codes)
        {
            Basic = Guard.AgainstLessThan(0, codes.Basic, "Basic rate is less than 0");
            Import = Guard.AgainstLessThan(0, codes.Import, "Import tax rate is less than 0");
            Sales = Guard.AgainstLessThan(0, codes.Sales, "Sales tax rate is less than 0");
        }
    }
}
