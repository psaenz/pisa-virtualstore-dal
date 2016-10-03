using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base;
using Pisa.VirtualStore.Models.Calculus;

namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.Calculus
{
    class CalculusOrderEntityConfiguration : BaseEntityConfiguration<CalculusOrder>
    {
        CalculusOrderEntityConfiguration() {
            MakeRequired(fk => fk.Order);
            MakeRequired(fk => fk.Store);
        }
    }
}
