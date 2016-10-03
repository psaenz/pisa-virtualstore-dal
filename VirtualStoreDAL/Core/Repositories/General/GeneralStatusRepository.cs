using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.General;
using System.Collections.Generic;
using System.Linq;

namespace Pisa.VirtualStore.Dal.Core.Repositories.General
{
    public class GeneralStatusRepository : BaseRepository<GeneralStatus>
    {
        public GeneralStatusRepository(VirtualStoreDbContext context) : base(context) { }
        public IEnumerable<GeneralStatus> GetAllByType(string type)
        {
            return this.Context.GeneralStatuses.Where(gs => gs.Type == type);
        }
        public GeneralStatus GetByTypeAndName(string type, string Name)
        {
            return this.Context.GeneralStatuses.Where(gs => gs.Type == type && gs.Name == Name).FirstOrDefault();
        }
    }
}
