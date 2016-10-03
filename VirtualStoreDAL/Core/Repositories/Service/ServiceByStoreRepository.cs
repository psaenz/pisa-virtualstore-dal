using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.Service;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Service
{
    public class ServiceByStoreRepository : BaseRepository<ServiceByStore>
    {
        public ServiceByStoreRepository(VirtualStoreDbContext context) : base(context) { }
    }
}
