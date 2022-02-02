using FluentAssertions;
using Moq;
using SalesTaxManager.Configuration;
using SalesTaxManager.Entities;
using SalesTaxManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace given_a_sales_tax_manager.configuration.test
{
    public class when_retrieving_tax_details
    {
        private readonly string _TaxValidationPath;
        private readonly TaxRates _TaxRates;
        public when_retrieving_tax_details()
        {
            _TaxValidationPath = @"Configuration\TestData\TaxValidation\";
            _TaxRates = new TaxRates(
                new Codes()
                {
                    Basic = 10,
                    Import = 5,
                    Sales = 0.05
                });
        }


        [Theory]
        [InlineData("MissingSection.json")]
        [InlineData("notevenafile")]
        public void then_raise_exception_when_corrupt_file(string invalidFile)
        {
            Action act = () => new TaxContent(
                $"{_TaxValidationPath}{invalidFile}",
                It.IsAny<IConverter<TaxRates, Codes>>());

            act.Should().Throw<Exception>();
        }


        [Fact]
        public void then_get_tax_codes()
        {
            //ARRANGE
            Mock<IConverter<TaxRates, Codes>> converterMOCK =
                new Mock<IConverter<TaxRates, Codes>>();

            converterMOCK.Setup(fn => fn.Convert(It.IsAny<Codes>()))
                .Returns(_TaxRates);

            var sut = new TaxContent(
              $"{_TaxValidationPath}ValidTaxConfiguration.json",
              converterMOCK.Object);

            //ACT
            sut.TaxRates.Should().BeEquivalentTo(_TaxRates);
            converterMOCK.Verify(fn => fn.Convert(It.IsAny<Codes>()), Times.Once);
        }



        [Fact]
        public void then_convert_valid_tax_codes()
        {
            //ARRANGE
            TaxContentConverter sut = 
                new TaxContentConverter();

            var codes = new  Codes()
                {
                    Basic = 10,
                    Import = 5,
                    Sales = 0.05
                };

            //ACT
            var taxACT = sut.Convert(codes);

            //ASSERT
            taxACT.Basic.Should().Be(_TaxRates.Basic);
            taxACT.Sales.Should().Be(_TaxRates.Sales);
            taxACT.Import.Should().Be(_TaxRates.Import);
        }

        [Theory]
        [InlineData(-1,5,5)]
        [InlineData(53.4, -1, 23)]
        [InlineData(53.4, 2, -5)]
        public void then_detect_invalid_tax_codes(
            double basic,
            double import,
            double sales)
        {
            //ARRANGE
            TaxContentConverter sut =
                new TaxContentConverter();

            Codes codesEXP = new Codes()
            {
                Basic = basic,
                Import = import,
                Sales = sales
            };


            //ACT + ASSERT
            Action act = () => sut.Convert(codesEXP);
            act.Should().Throw<Exception>();
        }
    }
}
