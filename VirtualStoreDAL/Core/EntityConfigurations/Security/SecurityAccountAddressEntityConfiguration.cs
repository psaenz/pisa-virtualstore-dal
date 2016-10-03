using Pisa.VirtualStore.Models.Security;
using System.Data.Entity.ModelConfiguration;
using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base;

namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.Security
{
    class SecurityAccountAddressEntityConfiguration : BaseEntityConfiguration<SecurityAccountAddress>
    {
        SecurityAccountAddressEntityConfiguration() {
            MakeRequired(fk => fk.ContactAddress);
            MakeRequired(fk => fk.SecurityAccount);
        }
    }
}
