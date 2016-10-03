using Pisa.VirtualStore.Models.Security;
using System.Data.Entity.ModelConfiguration;
using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base;

namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.Security
{
    class SecurityActionEntityConfiguration : BaseEntityConfiguration<Models.Security.SecurityAction>
    {
        SecurityActionEntityConfiguration() {
            MakeRequired(fk => fk.GeneralStatus);
            MakeOptional(fk => fk.SecurityActionParent, fk => fk.SecurityActionParentId);
        }
    }
}
