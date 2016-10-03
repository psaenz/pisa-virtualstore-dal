using Pisa.VirtualStore.Dal.Core.Repositories.Base;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Product
{
    public class ProductRepository : BaseRepository<Models.Product.Product>
    {
        public ProductRepository(VirtualStoreDbContext context) : base(context)
        {
        }
    }
}
