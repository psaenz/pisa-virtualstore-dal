using Pisa.VirtualStore.Dal.Core.Repositories.Base;
using Pisa.VirtualStore.Models.Contact;
using System.Linq;

namespace Pisa.VirtualStore.Dal.Core.Repositories.Contact
{
    public class ContactTypeRepository : BaseRepository<Models.Contact.ContactType>
    {
        public ContactTypeRepository(VirtualStoreDbContext context) : base(context)
        {
        }

        public ContactType GetByName(string name) {
            return this.Context.ContactsTypes.Where(c => c.Name == name).FirstOrDefault();
        }
    }
}
