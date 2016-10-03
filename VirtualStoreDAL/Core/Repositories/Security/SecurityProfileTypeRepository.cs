using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.Security;
using System.Linq;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Security
{
    public class SecurityProfileTypeRepository : BaseRepository<SecurityProfileType>
    {
        public SecurityProfileTypeRepository(VirtualStoreDbContext context) : base(context) { }

        public SecurityProfileType GetByName(string Name)
        {
            return this.Context.SecurityProfileTypes.Where(sp => sp.Name == Name).FirstOrDefault();
        }
    }
}
