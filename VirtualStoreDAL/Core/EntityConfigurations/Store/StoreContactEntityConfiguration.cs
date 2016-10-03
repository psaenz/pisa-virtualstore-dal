using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base;
using Pisa.VirtualStore.Models.Store;

namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.Store
{
    class StoreContactEntityConfiguration : BaseEntityConfiguration<Models.Store.StoreContact>
    {
        StoreContactEntityConfiguration() {
            MakeRequired(fk => fk.Contact);
            MakeRequired(fk => fk.Store);
        }
    }
}
