using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.Calculus;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Calculus
{
    public class CalculusBasketDetailRepository : BaseRepository<CalculusBasketDetail>
    {
        public CalculusBasketDetailRepository(VirtualStoreDbContext context) : base(context)
        {
        }
    }
}
