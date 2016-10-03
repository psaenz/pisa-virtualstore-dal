using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.Security;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Security
{
    public class SecurityDefaultProfileRepository : BaseRepository<SecurityDefaultProfile>
    {
        public SecurityDefaultProfileRepository(VirtualStoreDbContext context) : base(context) { }
    }
}
