using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.General;

namespace Pisa.VirtualStore.Dal.Core.Repositories.General
{
    public class GeneralMediaRepository : BaseRepository<GeneralMedia>
    {
        public GeneralMediaRepository(VirtualStoreDbContext context) : base(context) { }
    }
}
