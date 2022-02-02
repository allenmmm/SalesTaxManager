
namespace SalesTaxManager.Interfaces
{
    public interface IConverter<TSource, TDestination>
    {
        abstract TDestination Convert(TSource source_object);
        abstract TSource Convert(TDestination source_object);
    }
}
