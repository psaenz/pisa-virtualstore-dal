using Pisa.VirtualStore.Models.Security;
using System.Data.Entity.ModelConfiguration;
using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base;

namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.Security
{
    class SecurityDefaultProfileEntityConfiguration : BaseEntityConfiguration<Models.Security.SecurityDefaultProfile>
    {
        SecurityDefaultProfileEntityConfiguration() {
            MakeRequired(fk => fk.SecurityProfile);
            MakeRequired(fk => fk.SecurityProfileType);
        }
    }
}
