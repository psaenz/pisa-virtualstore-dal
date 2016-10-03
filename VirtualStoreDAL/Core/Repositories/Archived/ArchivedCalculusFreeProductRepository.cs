using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.Archived;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Archived
{
    public class ArchivedCalculusFreeProductRepository : BaseRepository<ArchivedCalculusFreeProduct>
    {
        public ArchivedCalculusFreeProductRepository(VirtualStoreDbContext context) : base(context) {
        }
    }
}
