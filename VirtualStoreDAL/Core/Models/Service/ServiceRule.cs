namespace Pisa.VirtualStore.Dal.Core.Models.Service
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ServiceRule
    {
        public int Id { get; set; }

        [NotMapped]
        public int IdServiceByStore { get; set; }

        [NotMapped]
        public int IdServiceZone { get; set; }

        public double ShoppingAmount { get; set; }

        public double ChargeByService { get; set; }

        public bool ApplyAsPercentage { get; set; }

        public virtual ServiceByStore ServiceByStore { get; set; }

        public virtual ServiceZone ServiceZone { get; set; }
    }
}
