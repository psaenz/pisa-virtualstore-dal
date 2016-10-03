using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.Security;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Security
{
    public class SecurityAccountContactRepository : BaseRepository<SecurityAccountContact>
    {
        public SecurityAccountContactRepository(VirtualStoreDbContext context) : base(context) { }
    }
}
