using FluentAssertions;
using Moq;
using SalesTaxManager.Configuration;
using SalesTaxManager.Entities;
using SalesTaxManager.Interfaces;
using System;
using System.Collections.Generic;
using Xunit;

namespace given_a_sales_tax_manager.configuration.test
{
    public class when_reading_products_exempt_tax
    {
        private readonly string _TaxValidationPath;
        private readonly ProductsExemptFromSalesTax _ProductsExemptFromTaxEXP;
        public when_reading_products_exempt_tax()
        {
            _TaxValidationPath = @"Configuration\TestData\ExemptProductsFromTaxValidation\";

            _ProductsExemptFromTaxEXP = new ProductsExemptFromSalesTax(
                new ProductsContent()
                {
                    Books = new List<string>() { "this book", "that book" },
                    Foods = new List<string>() { "brussles" },
                    Medicines = new List<string>() { "buttercup syrup" }
                });
        }

        [Fact]
        public void then_get_products_excempt_from_tax()
        {
            //ARRANGE
            Mock<IConverter<ProductsExemptFromSalesTax, ProductsContent>> converterMOCK =
                new Mock<IConverter<ProductsExemptFromSalesTax, ProductsContent>>();
             
 
            converterMOCK.Setup(fn =>
                fn.Convert(
                    It.IsAny<ProductsContent>()))
                    .Returns(_ProductsExemptFromTaxEXP);


            //ACT
            var sut = new ProductsExemptFromTaxContent(
                $"{_TaxValidationPath}ValidProductsExemptFromTaxConfiguration.json",
                converterMOCK.Object);

            //ASSERT
            converterMOCK.Verify(fn => fn.Convert(It.IsAny<ProductsContent>()), Times.Once);
            sut.ProductsExemptFromTax.Should().BeEquivalentTo(_ProductsExemptFromTaxEXP);
        }


        [Theory]
        [InlineData("MissingSection.json")]
        [InlineData("notevenafile")]
        public void then_raise_exception_when_corrupt_file(string invalidFile)
        {
            Action act = () => new ProductsExemptFromTaxContent(
                $"{_TaxValidationPath}{invalidFile}",
                It.IsAny<IConverter<ProductsExemptFromSalesTax, ProductsContent>>()
                );

            act.Should().Throw<Exception>();
        }

        [Fact]
        public void then_convert_valid_excempt_products()
        {
            //ARRANGE
            ProductsExemptFromTaxContentConverter sut =
                new ProductsExemptFromTaxContentConverter();

            var productsContent = new ProductsContent()
            {
                Books = new List<string>() { "this book", "that book" },
                Foods = new List<string>() { "brussles" },
                Medicines = new List<string>() { "buttercup syrup" }
            };

            //ACT
            var productsACT = sut.Convert(productsContent);

            //ASSERT
            productsACT.Books.Should()
                .BeEquivalentTo(_ProductsExemptFromTaxEXP.Books);
            productsACT.Foods.Should()
                .BeEquivalentTo(_ProductsExemptFromTaxEXP.Foods);
            productsACT.Medicines.Should()
                .BeEquivalentTo(_ProductsExemptFromTaxEXP.Medicines);
        }
    }
}
