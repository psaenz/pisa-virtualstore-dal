using Pisa.VirtualStore.Models.Security;
using System.Data.Entity.ModelConfiguration;
using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base;

namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.Security
{
    class SecurityAccountContactEntityConfiguration : BaseEntityConfiguration<SecurityAccountContact>
    {
        SecurityAccountContactEntityConfiguration() {
            MakeRequired(fk => fk.Contact);
            MakeRequired(fk => fk.SecurityAccount);
        }
    }
}
