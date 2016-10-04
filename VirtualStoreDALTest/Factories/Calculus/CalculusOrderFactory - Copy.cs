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
    class CalculusBasketDetailFactory : BaseEntityFactory<CalculusBasketDetail>
    {
        public override CalculusBasketDetail CreateInstance()
        {
            CalculusAppliedOffer calculusAppliedOffer = EntitiesFactory.CalculusAppliedOfferFactory.CreateInstance();
            CalculusOrder calculusOrder = calculusAppliedOffer.CalculusOrder;
            ClientBasketDetail clientBasketDetail = EntitiesFactory.ClientBasketDetailFactory.CreateInstance();
            return CreateInstance(1500, 3000, calculusAppliedOffer, calculusOrder, clientBasketDetail, 1, 2, true);
        }

        public CalculusBasketDetail CreateInstance(double amountWithOffer, double amountWithoutOffer, CalculusAppliedOffer calculusAppliedOffer,
            CalculusOrder calculusOrder, ClientBasketDetail clientBasketDetail, int countWithOffer, int countWithoutOffer, bool providedByStore)  {
            CalculusBasketDetail detail = new CalculusBasketDetail();
            detail.AmountWithOffer = amountWithOffer;
            detail.AmountWithoutOffer = amountWithoutOffer;
            detail.CalculusAppliedOffer = calculusAppliedOffer;
            detail.CalculusOrder = calculusOrder;
            detail.ClientBasketDetail = clientBasketDetail;
            detail.CountWithOffer = countWithOffer;
            detail.CountWithoutOffer = countWithoutOffer;
            detail.ProvidedByStore = providedByStore;
            return detail;
        }
    }
}
