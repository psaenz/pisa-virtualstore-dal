using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.Service;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Service
{
    public class ServiceZoneRepository : BaseRepository<ServiceZone>
    {
        public ServiceZoneRepository(VirtualStoreDbContext context) : base(context) { }
    }
}
