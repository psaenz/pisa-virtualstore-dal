using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.Calculus;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Calculus
{
    public class CalculusOrderRepository : BaseRepository<CalculusOrder>
    {
        public CalculusOrderRepository(VirtualStoreDbContext context) : base(context)
        {
        }
    }
}
