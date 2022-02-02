using FluentAssertions;
using SalesTaxManager.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace given_a_sales_tax_manager.test
{
    public class when_retrieving_tax_details
    {
        private readonly string _TaxValidationPath;
        public when_retrieving_tax_details()
        {
            _TaxValidationPath = @"TestData\TaxValidation\";
        }

        [Fact]
        public void then_get_tax_codes()
        {
            double dutyEXP = 10;
            double importEXP = 5;
            double salesEXP = 0.05;

            var sut = new TaxContent($"{_TaxValidationPath}ValidTaxConfiguration.json"); 

            sut.TaxParameters.Basic.Should().Be(dutyEXP);
            sut.TaxParameters.Import.Should().Be(importEXP);
            sut.TaxParameters.Sales.Should().Be(salesEXP);
        }

        [Theory]
        [InlineData("MissingSection.json")]
        [InlineData("notevenafile")]
        public void then_raise_exception_on(string invalidFile)
        {
            Action act = () => new TaxContent($"{_TaxValidationPath}{invalidFile}");

            act.Should().Throw<Exception>();
        }

    }
}
