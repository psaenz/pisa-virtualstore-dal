using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base;
using Pisa.VirtualStore.Models.Offer;

namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.Offer
{
    class OfferEntityConfiguration : BaseEntityConfiguration<Models.Offer.Offer>
    {
        OfferEntityConfiguration() {
            MakeRequired(fk => fk.GeneralMedia);
            MakeRequired(fk => fk.GeneralSchedule);
            MakeRequired(fk => fk.GeneralStatus);
            MakeRequired(fk => fk.Store);
        }
    }
}
