using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.Calculus;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Calculus
{
    public class CalculusServiceCostRepository : BaseRepository<CalculusServiceCost>
    {
        public CalculusServiceCostRepository(VirtualStoreDbContext context) : base(context)
        {
        }
    }
}
