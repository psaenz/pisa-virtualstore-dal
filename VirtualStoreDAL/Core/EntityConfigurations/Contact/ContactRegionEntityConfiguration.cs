using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base;
using Pisa.VirtualStore.Models.Contact;

namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.Contact
{
    class ContactRegionEntityConfiguration : BaseEntityConfiguration<Models.Contact.ContactRegion>
    {
        ContactRegionEntityConfiguration() {
            MakeOptional(fk => fk.ContactRegionParent, fk => fk.ContactRegionParentId);
            MakeRequired(fk => fk.GeneralStatus);
        }
    }
}
