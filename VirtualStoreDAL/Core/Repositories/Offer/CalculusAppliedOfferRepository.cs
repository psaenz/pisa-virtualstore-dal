using Pisa.VirtualStore.Dal.Core.Repositories.Base;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Offer
{
    public class OfferRepository : BaseRepository<Models.Offer.OfferInfo>
    {
        public OfferRepository(VirtualStoreDbContext context) : base(context)
        {
        }
    }
}
