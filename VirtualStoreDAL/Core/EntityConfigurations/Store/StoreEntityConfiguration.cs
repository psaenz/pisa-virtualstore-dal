using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base;
using Pisa.VirtualStore.Models.Store;

namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.Store
{
    class StoreEntityConfiguration : BaseEntityConfiguration<Models.Store.Store>
    {
        StoreEntityConfiguration() {
            MakeOptional(fk => fk.GeneralMediaLogo, fk => fk.GeneralMediaLogoId);
            MakeRequired(fk => fk.GeneralStatus);
            MakeOptional(fk => fk.StoreParent, fk => fk.StoreParentId);
        }
    }
}
