using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.Calculus;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Calculus
{
    public class CalculusAppliedOfferRepository : BaseRepository<CalculusAppliedOffer>
    {
        public CalculusAppliedOfferRepository(VirtualStoreDbContext context) : base(context)
        {
        }
    }
}
