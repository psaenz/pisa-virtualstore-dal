using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.Security;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Security
{
    public class SecurityUserRepository : BaseRepository<SecurityUser>
    {
        public SecurityUserRepository(VirtualStoreDbContext context) : base(context) { }
    }
}
