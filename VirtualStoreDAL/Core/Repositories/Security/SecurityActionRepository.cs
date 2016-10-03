using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.Security;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Security
{
    public class SecurityActionRepository : BaseRepository<SecurityAction>
    {
        public SecurityActionRepository(VirtualStoreDbContext context) : base(context) { }
    }
}
