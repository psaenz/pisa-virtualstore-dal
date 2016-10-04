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

namespace Pisa.VirtualStore.Dal.Test.Factories.Order
{
    class OfferInfoFactory : BaseEntityFactory<OfferInfo>
    {
        public override OfferInfo CreateInstance()
        {
            GeneralStatus status = OfferStatuses.GetInstance().EDITING;
            StoreInfo store = EntitiesFactory.StoreFactory.CreateInstance();
            return CreateInstance(null, null, status, 1, "2 x 1 Test Offer", store);
        }

        public OfferInfo CreateInstance(GeneralMedia media, GeneralSchedule schedule, GeneralStatus status, 
            int maximumPerOrder, string name, StoreInfo store) {

            OfferInfo offer = new OfferInfo();
            offer.GeneralMedia = media;
            offer.GeneralSchedule = schedule;
            offer.GeneralStatus = status;
            offer.MaximumPerOrder = maximumPerOrder;
            offer.Name = name;
            offer.Store = store;
            return offer;
        }
    }
}
