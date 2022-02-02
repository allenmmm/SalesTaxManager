using System;
using System.Collections.Generic;
using System.Text;

namespace SalesTaxManager.Interfaces
{
    public interface ITaxAndGoodsleReadService<TDestination, TSource>
    {
        TDestination ReadTax(
            IConverter<TDestination, TSource> converter);

        IEnumerable<TDestination> ReadExceTaxGoodsCategories(
            IConverter<TDestination, TSource> converter);
    }
}
