using System;
using Pisa.VirtualStore.Dal.Core.Helpers;
using Pisa.VirtualStore.Dal.Test.Factories.Base;
using Pisa.VirtualStore.Models.General;
using Pisa.VirtualStore.Models.Order;
using Pisa.VirtualStore.Models.Client;
using Pisa.VirtualStore.Models.Security;
using Pisa.VirtualStore.Models.Service;
using Pisa.VirtualStore.Models.Store;

namespace Pisa.VirtualStore.Dal.Test.Factories.Order
{
    class OrderFactory : BaseEntityFactory<OrderInfo>
    {
        public override OrderInfo CreateInstance()
        {
            ClientBasket basket = EntitiesFactory.ClientBasketFactory.CreateInstance();
            GeneralStatus status = OrderStatuses.GetInstance().PENDING;
            SecurityUser requestedBy = EntitiesFactory.SecurityUserFactory.CreateInstance();
            SecurityAccountAddress address = EntitiesFactory.SecurityAccountAddressFactory.CreateInstance();
            ServiceType serviceType = ServiceTypes.GetInstance().TO_HOME;
            StoreInfo store = EntitiesFactory.StoreFactory.CreateInstance();
            return CreateInstance(170000, basket, null, 
                DateTime.UtcNow, status, requestedBy, false, address, 
                serviceType, store);
        }

        public OrderInfo CreateInstance(double amount, ClientBasket basket, Nullable<DateTime> deliveredOn, 
            DateTime requestedOn, GeneralStatus gs, SecurityUser requestedBy, bool scheduled, 
            SecurityAccountAddress deliverTo, ServiceType serviceType, StoreInfo store) {
            OrderInfo order = new OrderInfo();
            order.Amount = amount;
            order.ClientBasket = basket;
            order.DateDelivered = deliveredOn;
            order.DateRequested = requestedOn;
            order.GeneralStatus = gs;
            order.RequestedByUser = requestedBy;
            order.Scheduled = scheduled;
            order.SecurityAccountAddress = deliverTo;
            order.ServiceType = serviceType;
            order.Store = store;
            return order;
        }
    }
}
