using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.Service;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Service
{
    public class ServiceTypeRepository : BaseRepository<ServiceType>
    {
        public ServiceTypeRepository(VirtualStoreDbContext context) : base(context) { }
    }
}
