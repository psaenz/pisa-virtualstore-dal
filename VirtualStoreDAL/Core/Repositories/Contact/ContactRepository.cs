using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.Contact;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Contact
{
    public class ContactRepository : BaseRepository<Models.Contact.Contact>
    {
        public ContactRepository(VirtualStoreDbContext context) : base(context)
        {
        }
    }
}
