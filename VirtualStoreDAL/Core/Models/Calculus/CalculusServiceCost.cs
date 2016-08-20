using Pisa.VirtualStore.Dal.Core.Models.Product;namespace Pisa.VirtualStore.Dal.Core.Models.Calculus
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Pisa.VirtualStore.Dal.Core.Models.Service;

    public partial class CalculusServiceCost
    {
        public int Id { get; set; }

        [NotMapped]
        public int IdCalculusOrder { get; set; }

        [NotMapped]
        public int IdServiceType { get; set; }

        public double ServiceCost { get; set; }

        public virtual CalculusOrder CalculusOrder { get; set; }

        public virtual ServiceType ServiceType { get; set; }
    }
}
