using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.General;
using System.Linq;

namespace Pisa.VirtualStore.Dal.Core.Repositories.General
{
    public class GeneralMediaTypeRepository : BaseRepository<GeneralMediaType>
    {
        public GeneralMediaTypeRepository(VirtualStoreDbContext context) : base(context) { }

        public GeneralMediaType GetByName(string name)
        {
            return this.Context.GeneralMediaTypes.Where(c => c.Name == name).FirstOrDefault();
        }
    }
}
