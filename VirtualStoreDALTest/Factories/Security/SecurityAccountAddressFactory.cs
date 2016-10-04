using Pisa.VirtualStore.Dal.Core.Helpers;
using Pisa.VirtualStore.Dal.Test.Factories.Base;
using Pisa.VirtualStore.Models.General;
using Pisa.VirtualStore.Models.Contact;
using Pisa.VirtualStore.Models.Security;

namespace Pisa.VirtualStore.Dal.Test.Factories.Security
{
    class SecurityAccountAddressFactory : BaseEntityFactory<SecurityAccountAddress>
    {
        public override SecurityAccountAddress CreateInstance()
        {
            SecurityAccount acc = EntitiesFactory.SecurityAccountFactory.CreateInstance();
            ContactAddress address = new ContactAddress {
                ContactRegion = EntitiesFactory.ContactRegionFactory.CreateInstance(),
                Details = "Test Address 123"};
            return CreateInstance(acc, address);
        }

        public SecurityAccountAddress CreateInstance(SecurityAccount account, ContactAddress address) {
            SecurityAccountAddress accAddress = new SecurityAccountAddress();
            accAddress.SecurityAccount = account;
            accAddress.ContactAddress = address;
            return accAddress;
        }
    }
}
