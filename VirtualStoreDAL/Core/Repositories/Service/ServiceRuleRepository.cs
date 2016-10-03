using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.Service;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Service
{
    public class ServiceRuleRepository : BaseRepository<ServiceRule>
    {
        public ServiceRuleRepository(VirtualStoreDbContext context) : base(context) { }
    }
}
