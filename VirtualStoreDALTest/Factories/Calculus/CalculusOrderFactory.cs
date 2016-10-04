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
    class CalculusOrderFactory : BaseEntityFactory<CalculusOrder>
    {
        public override CalculusOrder CreateInstance()
        {
            OrderInfo order = EntitiesFactory.OrderFactory.CreateInstance();
            StoreInfo store = EntitiesFactory.StoreFactory.CreateInstance();
            return CreateInstance(DateTime.UtcNow, order, store);
        }

        public CalculusOrder CreateInstance(DateTime calculusDate, OrderInfo order, StoreInfo store) {
            CalculusOrder calculusOrder = new CalculusOrder();
            calculusOrder.CalculusDate = calculusDate;
            calculusOrder.Order = order;
            calculusOrder.Store = store;
            return calculusOrder;
        }
    }
}
