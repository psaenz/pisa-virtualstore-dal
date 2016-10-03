using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.Calculus;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Calculus
{
    public class CalculusFreeProductRepository : BaseRepository<CalculusFreeProduct>
    {
        public CalculusFreeProductRepository(VirtualStoreDbContext context) : base(context)
        {
        }
    }
}
