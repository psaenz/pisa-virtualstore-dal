using Pisa.VirtualStore.Models.Security;
using System.Data.Entity.ModelConfiguration;
using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base;

namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.Security
{
    class SecurityProfileActionEntityConfiguration : BaseEntityConfiguration<Models.Security.SecurityProfileAction>
    {
        SecurityProfileActionEntityConfiguration() {
            MakeRequired(fk => fk.SecurityAction);
            MakeRequired(fk => fk.SecurityProfile);
        }
    }
}
