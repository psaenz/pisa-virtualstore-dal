using Pisa.VirtualStore.Dal.Core.Repositories.Base;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Store
{
    public class StoreProductRepository : BaseRepository<Models.Store.StoreProduct>
    {
        public StoreProductRepository(VirtualStoreDbContext context) : base(context) { }
    }
}
