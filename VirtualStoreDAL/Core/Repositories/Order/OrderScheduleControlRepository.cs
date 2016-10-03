using Pisa.VirtualStore.Dal.Core.Repositories.Base;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Order
{
    public class OrderScheduleControlRepository : BaseRepository<Models.Order.OrderScheduleControl>
    {
        public OrderScheduleControlRepository(VirtualStoreDbContext context) : base(context)
        {
        }
    }
}
