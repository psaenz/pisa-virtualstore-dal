using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.Security;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Security
{
    public class SecurityAccountStoreRepository : BaseRepository<SecurityAccountStore>
    {
        public SecurityAccountStoreRepository(VirtualStoreDbContext context) : base(context) { }
    }
}
