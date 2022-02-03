using FluentAssertions;
using Moq;
using SalesTaxManager.Configuration;
using SalesTaxManager.Entities;
using SalesTaxManager.Interfaces;
using System;
using Xunit;

namespace given_a_sales_tax_manager.configuration.test
{
    public class when_reading_tax_details
    {
        private readonly string _TaxValidationPath;
        private readonly TaxRates _TaxRates;
        public when_reading_tax_details()
        {
            _TaxValidationPath = @"Configuration\TestData\TaxValidation\";
            _TaxRates = new TaxRates(
                new Codes()
                {
                    Sales = 10,
                    Import = 5,
                    SalesRoundingFactor = 0.05m
                });
        }

        [Theory]
        [InlineData("MissingSection.json")]
        [InlineData("notevenafile")]
        public void then_raise_exception_when_corrupt_file(string invalidFile)
        {
            //ARRANGE
            Action act = () => new TaxContent(
                $"{_TaxValidationPath}{invalidFile}",
                It.IsAny<IConverter<TaxRates, Codes>>());
            //ACT + ASSERT
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

            //ACT + ASSERT
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
                    Sales = 10,
                    Import = 5,
                    SalesRoundingFactor = 0.05m
                };

            //ACT
            var taxACT = sut.Convert(codes);

            //ASSERT
            taxACT.SalesRoundingFactor.Should().Be(_TaxRates.SalesRoundingFactor);
            taxACT.Sales.Should().Be(_TaxRates.Sales);
            taxACT.Import.Should().Be(_TaxRates.Import);
        }

        [Theory]
        [InlineData(-1,5,5)]
        [InlineData(53.4, -1, 23)]
        [InlineData(53.4, 2, -5)]
        public void then_detect_invalid_tax_codes(
            decimal basic,
            decimal import,
            decimal sales)
        {
            //ARRANGE
            TaxContentConverter sut =
                new TaxContentConverter();

            Codes codesEXP = new Codes()
            {
                Sales = basic,
                Import = import,
                SalesRoundingFactor = sales
            };

            //ACT + ASSERT
            Action act = () => sut.Convert(codesEXP);
            act.Should().Throw<Exception>();
        }
    }
}
