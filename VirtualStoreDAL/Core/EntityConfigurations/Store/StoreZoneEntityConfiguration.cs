using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base;
using Pisa.VirtualStore.Models.Store;

namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.Store
{
    class StoreZoneEntityConfiguration : BaseEntityConfiguration<Models.Store.StoreZone>
    {
        StoreZoneEntityConfiguration() {
            MakeRequired(fk => fk.ContactRegion);
            MakeRequired(fk => fk.ServiceZone);
        }
    }
}
