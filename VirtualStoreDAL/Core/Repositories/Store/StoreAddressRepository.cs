using Pisa.VirtualStore.Dal.Core.Repositories.Base;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Store
{
    public class StoreAddressRepository : BaseRepository<Models.Store.StoreAddress>
    {
        public StoreAddressRepository(VirtualStoreDbContext context) : base(context) { }
    }
}
