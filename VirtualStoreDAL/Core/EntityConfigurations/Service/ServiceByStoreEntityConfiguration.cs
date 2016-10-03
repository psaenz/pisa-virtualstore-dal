using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base;
using Pisa.VirtualStore.Models.Service;

namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.Service
{
    class ServiceByStoreEntityConfiguration : BaseEntityConfiguration<ServiceByStore>
    {
        ServiceByStoreEntityConfiguration() {
            MakeRequired(fk => fk.GeneralStatus);
            MakeRequired(fk => fk.ServiceType);
            MakeRequired(fk => fk.Store);
        }
    }
}
