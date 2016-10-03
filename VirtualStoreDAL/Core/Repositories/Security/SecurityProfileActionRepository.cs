using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.Security;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Security
{
    public class SecurityProfileActionRepository : BaseRepository<SecurityProfileAction>
    {
        public SecurityProfileActionRepository(VirtualStoreDbContext context) : base(context) { }
    }
}
