using SalesTaxManager.Entities;
using SalesTaxManager.Interfaces;

namespace SalesTaxManager.Configuration
{
    public class ItemContent
    {
        public string Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool  Imported { get; set; }
    }

    public class TrolleyContent : File<ItemContent>
    {
        public Trolley Trolley { get; private set; }

        public TrolleyContent(
            string fileName,
            IConverterList<Trolley, ItemContent> converter) : base(fileName, "Trolley")
        {
            Trolley = converter.Convert(_Data);
        }
    }
}
