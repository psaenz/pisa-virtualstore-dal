using System;
using Pisa.VirtualStore.Dal.Core.Helpers;
using Pisa.VirtualStore.Dal.Test.Factories.Base;
using Pisa.VirtualStore.Models.General;
using Pisa.VirtualStore.Models.Order;
using Pisa.VirtualStore.Models.Offer;
using Pisa.VirtualStore.Models.Client;
using Pisa.VirtualStore.Models.Security;
using Pisa.VirtualStore.Models.Service;
using Pisa.VirtualStore.Models.Store;
using Pisa.VirtualStore.Models.Calculus;

namespace Pisa.VirtualStore.Dal.Test.Factories.Order
{
    class CalculusAppliedOfferFactory : BaseEntityFactory<CalculusAppliedOffer>
    {
        public override CalculusAppliedOffer CreateInstance()
        {
            CalculusOrder calculusOrder = EntitiesFactory.CalculusOrderFactory.CreateInstance();
            OfferInfo offer = EntitiesFactory.OfferInfoFactory.CreateInstance();
            return CreateInstance(calculusOrder, 2, offer);
        }

        public CalculusAppliedOffer CreateInstance(CalculusOrder calculusOrder, int numberApplied, OfferInfo offer) {
            CalculusAppliedOffer appliedOffer = new CalculusAppliedOffer();
            appliedOffer.CalculusOrder = calculusOrder;
            appliedOffer.NumberApplied = numberApplied;
            appliedOffer.Offer = offer;
            return appliedOffer;
        }
    }
}
