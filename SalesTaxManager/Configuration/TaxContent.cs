using SalesTaxManager.Entities;
using SalesTaxManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalesTaxManager.Configuration
{
    public class Codes
    {
        public double Basic { get; set; }
        public double Import { get; set; }
        public double Sales { get; set; }
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
