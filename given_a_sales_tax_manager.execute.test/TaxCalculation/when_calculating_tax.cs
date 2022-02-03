using FluentAssertions;
using SalesTaxManager.Configuration;
using SalesTaxManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace given_a_sales_tax_manager.test.TaxCalculation
{
    public class when_calculating_tax
    {
        private readonly Codes _CodesEXP = null;
        private readonly ProductsContent _ProductsContentEXP;
        private readonly List<ItemContent> _ItemsContextEXP = null;

        public when_calculating_tax()
        {
            _CodesEXP = new Codes()
            {
                Import = 5,
                Sales = 10,
                SalesRoundingFactor = 0.05m
            };

            _ProductsContentEXP = new ProductsContent()
            {
                Foods = new List<string>{
                    "box of chocolates",
                    "chocolate bar"
                },
                Books = new List<string>{
                    "book",
                },
                Medicines = new List<string> {
                    "headache pills"
                }
            };

            _ItemsContextEXP = new List<ItemContent>(){
                new ItemContent(){
                    Imported = true,
                    Price = 27.99m,
                    Product = "bottle of perfume",
                    Quantity = 1
                },
                new ItemContent(){
                    Imported = false,
                    Price = 18.99m,
                    Product = "bottle of perfume",
                    Quantity = 1
                },
                new ItemContent(){
                    Imported = false,
                    Price = 9.75m,
                    Product = "packet of headache pills",
                    Quantity = 1         
                },
                new ItemContent(){
                    Imported = true,
                    Price = 11.25m,
                    Product = "box of chocolates",
                    Quantity = 1
                }
            };
        }

    

        [Fact]
        public void then_set_payable_tax_on_an_item()
        {
            //ARRANGE
            var sut = new Item(_ItemsContextEXP.First());

            //ACT 
            sut.SetTax(4.44m);

            //ASSERT
            sut.PriceWithTax.Should().Be(4.44m + _ItemsContextEXP.First().Price);
        }

        [Fact]
        public void then_detect_invalid_payable_tax()
        {
            //ARRANGE
            var sut = new Item(_ItemsContextEXP.First());

            //ACT 
            Action act = () => sut.SetTax(-4.44m);

            //ASSERT
            act.Should().Throw<Exception>();
        }


        [Fact]
        public void then_calculate_trolley_tax()
        {
            //ARRANGE
            var totalItemTaxEXP = new List<decimal>(){
                32.19m,
                20.89m,
                9.75m,
                11.81m,
            };
            var taxRatesEXP = new TaxRates(_CodesEXP);
            var sut = new Trolley(_ItemsContextEXP);

            var productsExemptFromTax = new 
                ProductsExemptFromSalesTax(_ProductsContentEXP);

            //ACT
            sut.CalculateTax(taxRatesEXP, productsExemptFromTax);
            
            //ASSERT
            int index = 0;
            foreach(var i in sut.Items)
            {
                totalItemTaxEXP.ElementAt(index++).Should().Be(i.PriceWithTax);
           }
            sut.TrolleyTax.Should().Be(6.66m);


        }
    }
}
