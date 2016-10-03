using Pisa.VirtualStore.Models.Security;
using System.Data.Entity.ModelConfiguration;
using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base;

namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.Security
{
    class SecurityUserEntityConfiguration : BaseEntityConfiguration<SecurityUser>
    {
        SecurityUserEntityConfiguration() {
            MakeRequired(fk => fk.GeneralStatus);
            MakeRequired(fk => fk.SecurityPerson);
            MakeOptional(fk => fk.LastAccountUsed, fk => fk.LastAccountUsedId);
        }
    }
}
