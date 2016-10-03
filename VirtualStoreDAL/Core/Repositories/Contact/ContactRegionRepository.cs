using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.Contact;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Contact
{
    public class ContactRegionRepository : BaseRepository<Models.Contact.ContactRegion>
    {
        public ContactRegionRepository(VirtualStoreDbContext context) : base(context)
        {
        }
    }
}
