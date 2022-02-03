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
    public class when_reading_trolleys
    {
        private readonly string _TaxValidationPath;
        private readonly Trolley _Trolley;
        private readonly IEnumerable<ItemContent> _ItemsEXP;
        public when_reading_trolleys()
        {
            _TaxValidationPath = @"Configuration\TestData\TrolleyValidation\";

            _ItemsEXP = new List<ItemContent>()
                {
                    new ItemContent(){
                        Imported = false,
                        Price=10.67m,
                        Product = "Cheese",
                        Quantity= 5
                    },
                    new ItemContent(){
                        Imported = true,
                        Price=6.25m,
                        Product = "Toast",
                        Quantity=99
                    },
                };

            _Trolley = new Trolley(_ItemsEXP);
        }

        [Fact]
        public void then_get_tax_codes()
        {
            //ARRANGE
            Mock<IConverterList<Trolley, ItemContent>> converterMOCK =
                new Mock<IConverterList<Trolley, ItemContent>>();

            converterMOCK.Setup(fn => fn.Convert(It.IsAny<IEnumerable<ItemContent>>()))
                .Returns(_Trolley);

            //ACT
            var sut = new TrolleyContent(
                $"{_TaxValidationPath}ValidTrolleyConfiguration.json",
                converterMOCK.Object);

            //ASSERT
            sut.Trolley.Should().BeEquivalentTo(_Trolley);
            converterMOCK.Verify(fn => 
                fn.Convert(It.IsAny<IEnumerable<ItemContent>>()), Times.Once);
        }

        [Theory]
        [InlineData("MissingSection.json")]
        [InlineData("notevenafile")]
        public void then_raise_exception_on(string invalidFile)
        {
            Action act = () => new TrolleyContent(
                $"{_TaxValidationPath}{invalidFile}",
                It.IsAny<IConverterList<Trolley, ItemContent>>());

            act.Should().Throw<Exception>();
        }

        [Fact]
        public void then_convert_valid_trolley()
        {
            //ARRANGE
            TrolleyContentConverter sut =
                new TrolleyContentConverter();

            var items = new List<ItemContent>()
            {
                new ItemContent(){
                     Imported = false,
                     Price = 8.76m,
                     Product = "Guitars",
                     Quantity = 5
                },
                new ItemContent(){
                     Imported = true,
                     Price = 18.76m,
                     Product = "Plus",
                     Quantity = 2
                },
            };

            //ACT
            var trolleyACT = sut.Convert(_ItemsEXP);

            //ASSERT
            trolleyACT.Items.Should().NotBeEmpty();
        }

        [Theory]
        [InlineData(false,-34.33,"cheese", 5)]
        [InlineData(false, 34.33, null, 5)]
        [InlineData(false, 34.33, "", 5)]
        [InlineData(false, 34.33, "cheese", -5)]
        public void then_detect_invalid_trolley(
            bool imported,
            decimal price,
            string product,
            int quantity)
        {
            //ARRANGE
            TrolleyContentConverter sut =
                new TrolleyContentConverter();

            var items = new List<ItemContent>()
            {
                new ItemContent(){
                     Imported = imported,
                     Price = price,
                     Product =product,
                     Quantity = quantity
                }
            };
            //ACT + ASSERT
            Action act = () => sut.Convert(items);
            act.Should().Throw<Exception>();
        }

    }
}
