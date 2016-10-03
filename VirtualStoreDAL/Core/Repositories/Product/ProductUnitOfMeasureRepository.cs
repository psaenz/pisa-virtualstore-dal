using Pisa.VirtualStore.Dal.Core.Repositories.Base;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Product
{
    public class ProductUnitOfMeasureRepository : BaseRepository<Models.Product.ProductUnitOfMeasure>
    {
        public ProductUnitOfMeasureRepository(VirtualStoreDbContext context) : base(context)
        {
        }
    }
}
