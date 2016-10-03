using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base;
using Pisa.VirtualStore.Models.Store;

namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.Store
{
    class StoreProductEntityConfiguration : BaseEntityConfiguration<Models.Store.StoreProduct>
    {
        StoreProductEntityConfiguration() {
            MakeRequired(fk => fk.Product);
            MakeRequired(fk => fk.Store);
        }
    }
}
