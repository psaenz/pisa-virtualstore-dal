using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pisa.VirtualStore.Models.Archived;
using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base;

namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.Archived
{
    class ArchivedCalculusOrderEntityConfiguration : BaseEntityConfiguration<ArchivedCalculusOrder>
    {
        ArchivedCalculusOrderEntityConfiguration() {
            MakeRequired(fk => fk.Order);
            MakeRequired(fk => fk.Store);
        }
    }
}
