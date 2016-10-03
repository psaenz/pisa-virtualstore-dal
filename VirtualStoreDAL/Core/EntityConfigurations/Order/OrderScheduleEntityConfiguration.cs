using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pisa.VirtualStore.Dal.Core.EntityConfigurations.Base;
using Pisa.VirtualStore.Models.Order;


namespace Pisa.VirtualStore.Dal.Core.EntityConfigurations.Order
{
    class OrderScheduleEntityConfiguration : BaseEntityConfiguration<Models.Order.OrderSchedule>
    {
        OrderScheduleEntityConfiguration() {
            MakeRequired(fk => fk.GeneralSchedule);
            MakeRequired(fk => fk.GeneralStatus);
            MakeRequired(fk => fk.Order);
            MakeRequired(fk => fk.SecurityUser);
        }
    }
}
