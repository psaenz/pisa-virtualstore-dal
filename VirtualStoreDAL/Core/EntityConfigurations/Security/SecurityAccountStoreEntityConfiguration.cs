using Pisa.VirtualStore.Models.Security;
using System.Data.Entity.ModelConfiguration;
using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base;

namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.Security
{
    class SecurityAccountStoreEntityConfiguration : BaseEntityConfiguration<SecurityAccountStore>
    {
        SecurityAccountStoreEntityConfiguration() {
            MakeRequired(fk => fk.Store);
            MakeRequired(fk => fk.SecurityAccount);
        }
    }
}
