using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base;
using Pisa.VirtualStore.Models.Calculus;

namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.Calculus
{
    class CalculusBasketDetailEntityConfiguration : BaseEntityConfiguration<CalculusBasketDetail>
    {
        CalculusBasketDetailEntityConfiguration() {
            MakeRequired(fk => fk.CalculusOrder);
            MakeRequired(fk => fk.CalculusAppliedOffer);
            MakeRequired(fk => fk.ClientBasketDetail);
        }
    }
}
