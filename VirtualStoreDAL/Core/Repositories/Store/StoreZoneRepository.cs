using Pisa.VirtualStore.Dal.Core.Repositories.Base;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Store
{
    public class StoreZoneRepository : BaseRepository<Models.Store.StoreZone>
    {
        public StoreZoneRepository(VirtualStoreDbContext context) : base(context) { }
    }
}
