using Pisa.VirtualStore.Dal.Core.Repositories.Base;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Offer
{
    public class OffersDetailRepository : BaseRepository<Models.Offer.OffersDetail>
    {
        public OffersDetailRepository(VirtualStoreDbContext context) : base(context)
        {
        }
    }
}
