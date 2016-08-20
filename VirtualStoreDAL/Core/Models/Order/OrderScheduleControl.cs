namespace Pisa.VirtualStore.Dal.Core.Models.Order
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderScheduleControl
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int OrdersGenerated { get; set; }
    }
}
