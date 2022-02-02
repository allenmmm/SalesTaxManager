using Moq;
using SalesTaxManager.ConsoleService;
using System;
using Xunit;
using SalesTaxManager;

namespace given_a_sales_tax_manager.execute.test
{
    public class when_launching_the_application
    {
        [Fact]
        public void then_invoke_tax_manager_execution()
        {
            Mock<IConsole> consoleMOCK = new Mock<IConsole>();

            SalesTaxRunner.Execute(consoleMOCK.Object);

            consoleMOCK.Verify(fn =>
                fn.WriteLine(It.IsAny<string>()), Times.AtLeastOnce);
        }
    }
}
