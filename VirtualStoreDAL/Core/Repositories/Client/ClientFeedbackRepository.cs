using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.Client;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Client
{
    public class ClientFeedbackRepository : BaseRepository<ClientFeedback>
    {
        public ClientFeedbackRepository(VirtualStoreDbContext context) : base(context)
        {
        }
    }
}
