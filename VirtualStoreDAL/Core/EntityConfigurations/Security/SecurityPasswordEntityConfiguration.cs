using Pisa.VirtualStore.Models.Security;
using System.Data.Entity.ModelConfiguration;
using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base;

namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.Security
{
    class SecurityPasswordEntityConfiguration : BaseEntityConfiguration<Models.Security.SecurityPassword>
    {
        SecurityPasswordEntityConfiguration() {
            MakeRequired(fk => fk.SecurityUser);
        }
    }
}
