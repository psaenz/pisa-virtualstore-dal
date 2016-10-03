using Pisa.VirtualStore.Dal.Core.Repositories.Base;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Store
{
    public class StoreRepository : BaseRepository<Models.Store.Store>
    {
        public StoreRepository(VirtualStoreDbContext context) : base(context) { }
    }
}
