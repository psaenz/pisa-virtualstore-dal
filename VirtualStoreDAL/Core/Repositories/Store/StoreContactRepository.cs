using Pisa.VirtualStore.Dal.Core.Repositories.Base;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Store
{
    public class StoreContactRepository : BaseRepository<Models.Store.StoreContact>
    {
        public StoreContactRepository(VirtualStoreDbContext context) : base(context) { }
    }
}
