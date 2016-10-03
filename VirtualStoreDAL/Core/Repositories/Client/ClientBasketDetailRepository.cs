using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.Client;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Client
{
    public class ClientBasketDetailRepository : BaseRepository<ClientBasketDetail>
    {
        public ClientBasketDetailRepository(VirtualStoreDbContext context) : base(context)
        {
        }
    }
}
