using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base;
using Pisa.VirtualStore.Models.Order;


namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.Order
{
    class OrderEntityConfiguration : BaseEntityConfiguration<Models.Order.OrderInfo>
    {
        OrderEntityConfiguration() {
            MakeRequired(fk => fk.ClientBasket);
            MakeRequired(fk => fk.GeneralStatus);
            MakeRequired(fk => fk.ServiceType);
            MakeRequired(fk => fk.RequestedByUser);
            MakeRequired(fk => fk.SecurityAccountAddress);
            MakeRequired(fk => fk.Store);
        }
    }
}
