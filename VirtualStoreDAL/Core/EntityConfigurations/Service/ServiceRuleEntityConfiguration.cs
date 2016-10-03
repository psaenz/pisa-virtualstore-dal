using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base;
using Pisa.VirtualStore.Models.Service;

namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.Service
{
    class ServiceRuleEntityConfiguration : BaseEntityConfiguration<ServiceRule>
    {
        ServiceRuleEntityConfiguration() {
            MakeRequired(fk => fk.ServiceByStore);
            MakeRequired(fk => fk.ServiceZone);
        }
    }
}
