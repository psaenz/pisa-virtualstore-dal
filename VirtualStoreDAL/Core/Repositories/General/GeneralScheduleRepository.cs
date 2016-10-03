using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.General;

namespace Pisa.VirtualStore.Dal.Core.Repositories.General
{
    public class GeneralScheduleRepository : BaseRepository<GeneralSchedule>
    {
        public GeneralScheduleRepository(VirtualStoreDbContext context) : base(context) { }
    }
}
