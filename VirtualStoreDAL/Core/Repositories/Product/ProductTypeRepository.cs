using Pisa.VirtualStore.Dal.Core.Repositories.Base;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Product
{
    public class ProductTypeRepository : BaseRepository<Models.Product.ProductType>
    {
        public ProductTypeRepository(VirtualStoreDbContext context) : base(context)
        {
        }
    }
}
