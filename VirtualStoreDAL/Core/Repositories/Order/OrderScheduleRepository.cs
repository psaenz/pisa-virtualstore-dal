using Pisa.VirtualStore.Dal.Core.Repositories.Base;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Order
{
    public class OrderScheduleRepository : BaseRepository<Models.Order.OrderSchedule>
    {
        public OrderScheduleRepository(VirtualStoreDbContext context) : base(context)
        {
        }
    }
}
