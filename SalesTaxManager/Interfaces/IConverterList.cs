using System;
using System.Collections.Generic;
using System.Text;

namespace SalesTaxManager.Interfaces
{
    public interface IConverterList<TDestination, TSource>
    {
        abstract TDestination Convert(IEnumerable<TSource> source_object);
        abstract IEnumerable<TSource> Convert(TDestination source_object);
    }
}
