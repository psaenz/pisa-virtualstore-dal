using Pisa.VirtualStore.Models.Security;
using System.Data.Entity.ModelConfiguration;
using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base;

namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.Security
{
    class SecurityAccountEntityConfiguration : BaseEntityConfiguration<SecurityAccount>
    {
        SecurityAccountEntityConfiguration() {
            MakeRequired(fk => fk.GeneralStatus);
            MakeRequired(fk => fk.SecurityUserOwner);
        }
    }
}
