using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pisa.VirtualStore.Models.Audit;
using Pisa.VirtualStore.Dal.Core.Repositories.Base;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Audit
{
    public class AuditAuthorRepository : BaseRepository<AuditAuthor>
    {
        public AuditAuthorRepository(VirtualStoreDbContext context) : base(context) { }
    }
}
