using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base;
using Pisa.VirtualStore.Models.Contact;

namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.Contact
{
    class ContactAddressEntityConfiguration : BaseEntityConfiguration<ContactAddress>
    {
        ContactAddressEntityConfiguration() {
            MakeRequired(fk => fk.ContactRegion);
        }
    }
}
