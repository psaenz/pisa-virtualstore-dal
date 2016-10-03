using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.Security;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Security
{
    public class SecurityAccountRepository : BaseRepository<SecurityAccount>
    {
        public SecurityAccountRepository(VirtualStoreDbContext context) : base(context) { }
    }
}
