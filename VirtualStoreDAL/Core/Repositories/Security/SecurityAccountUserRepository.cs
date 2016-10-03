using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.Security;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Security
{
    public class SecurityAccountUserRepository : BaseRepository<SecurityAccountUser>
    {
        public SecurityAccountUserRepository(VirtualStoreDbContext context) : base(context) { }
    }
}
