using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base;
using Pisa.VirtualStore.Models.Service;

namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.Service
{
    class ServiceZoneEntityConfiguration : BaseEntityConfiguration<ServiceZone>
    {
        ServiceZoneEntityConfiguration()
        {
            MakeRequired(fk => fk.Store);
        }
    }
}
