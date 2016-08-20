namespace Pisa.VirtualStore.Dal.Core.Models.Calculus
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Pisa.VirtualStore.Dal.Core.Models.Product;

    public partial class CalculusFreeProduct
    {
        public int Id { get; set; }

        [NotMapped]
        public int IdCalculusOrder { get; set; }

        [NotMapped]
        public int IdProduct { get; set; }

        public int Quantity { get; set; }

        public virtual CalculusOrder CalculusOrder { get; set; }

        public virtual Product Product { get; set; }
    }
}
