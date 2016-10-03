using Pisa.VirtualStore.Dal.Core.Repositories.Base;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Product
{
    public class ProductBrandRepository : BaseRepository<Models.Product.ProductBrand>
    {
        public ProductBrandRepository(VirtualStoreDbContext context) : base(context)
        {
        }
    }
}
