using Pisa.VirtualStore.Models.Security;
using System.Data.Entity.ModelConfiguration;
using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base;

namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.Security
{
    class SecurityProfileEntityConfiguration : BaseEntityConfiguration<Models.Security.SecurityProfile>
    {
        SecurityProfileEntityConfiguration() {
            MakeRequired(fk => fk.GeneralStatus);
            //MakeRequired(fk => fk.SecurityAccountCreator);
            MakeRequired(fk => fk.SecurityProfileType);
            MakeOptional(fk => fk.SecurityProfileParent, fk => fk.SecurityProfileParentId);
        }
    }
}
