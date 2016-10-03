using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.Security;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Security
{
    public class SecurityProfileRepository : BaseRepository<SecurityProfile>
    {
        public SecurityProfileRepository(VirtualStoreDbContext context) : base(context) { }
    }
}
