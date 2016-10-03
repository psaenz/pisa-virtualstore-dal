using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.Security;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Security
{
    public class SecurityPasswordRepository : BaseRepository<SecurityPassword>
    {
        public SecurityPasswordRepository(VirtualStoreDbContext context) : base(context) { }
    }
}
