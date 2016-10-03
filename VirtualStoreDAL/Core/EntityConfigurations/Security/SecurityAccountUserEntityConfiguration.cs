using Pisa.VirtualStore.Models.Security;
using System.Data.Entity.ModelConfiguration;
using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base;

namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.Security
{
    class SecurityAccountUserEntityConfiguration : BaseEntityConfiguration<Models.Security.SecurityAccountUser>
    {
        SecurityAccountUserEntityConfiguration() {
            MakeRequired(fk => fk.SecurityProfile);
            MakeRequired(fk => fk.SecurityUser);
            MakeRequired(fk => fk.SecurityAccount);
        }
    }
}
