using Pisa.VirtualStore.Dal.Test.Factories.Base;
using Pisa.VirtualStore.Dal.Core.Helpers;
using Pisa.VirtualStore.Models.Contact;
using Pisa.VirtualStore.Models.General;

namespace Pisa.VirtualStore.Dal.Test.Factories.Contact
{
    class ContactRegionFactory : BaseEntityFactory<ContactRegion>
    {
        public override ContactRegion CreateInstance()
        {
            return CreateInstance("Region Test", ContactRegionStatuses.GetInstance().ACTIVE, null);
        }

        public ContactRegion CreateInstance(string name, GeneralStatus gs, ContactRegion parent) {
            ContactRegion region = new ContactRegion();
            region.ContactRegionParent = parent;
            region.GeneralStatus = gs;
            region.Name = name;
            return region;
        }
    }
}
