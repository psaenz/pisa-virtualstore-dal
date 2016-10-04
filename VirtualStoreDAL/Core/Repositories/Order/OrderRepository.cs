using Pisa.VirtualStore.Dal.Core.Repositories.Base;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Order
{
    public class OrderRepository : BaseRepository<Models.Order.OrderInfo>
    {
        public OrderRepository(VirtualStoreDbContext context) : base(context)
        {
        }
    }
}
