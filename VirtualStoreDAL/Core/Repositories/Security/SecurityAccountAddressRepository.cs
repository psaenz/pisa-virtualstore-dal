using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.Security;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Security
{
    public class SecurityAccountAddressRepository : BaseRepository<SecurityAccountAddress>
    {
        public SecurityAccountAddressRepository(VirtualStoreDbContext context) : base(context) { }
    }
}
