using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalesTaxManager.Configuration
{
    public class TaxParameters
    {
        public double Basic { get; set; }
        public double Import { get; set; }
        public double Sales { get; set; }
    }

    public class TaxContent: File<TaxParameters> 
    {
        public TaxParameters TaxParameters { get; private set; }
        public TaxContent(
            string fileName) : base(fileName, "TaxParameters")
        {
            TaxParameters = new TaxParameters()
            {
                Basic = _Data.First().Basic,
                Import = _Data.First().Import,
                Sales = _Data.First().Sales
            };
        }
    }
}
