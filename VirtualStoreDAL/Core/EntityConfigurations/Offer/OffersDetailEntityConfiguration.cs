using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base;
using Pisa.VirtualStore.Models.Offer;

namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.Offer
{
    class OffersDetailEntityConfiguration : BaseEntityConfiguration<Models.Offer.OffersDetail>
    {
        OffersDetailEntityConfiguration() {
            MakeRequired(fk => fk.Offer);
            MakeRequired(fk => fk.Product);
        }
    }
}
