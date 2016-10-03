using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.Client;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Client
{
    public class ClientBasketRepository : BaseRepository<ClientBasket>
    {
        public ClientBasketRepository(VirtualStoreDbContext context) : base(context)
        {
        }
    }
}
