using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.Contact;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Contact
{
    public class ContactAddressRepository : BaseRepository<Models.Contact.ContactAddress>
    {
        public ContactAddressRepository(VirtualStoreDbContext context) : base(context)
        {
        }
    }
}
