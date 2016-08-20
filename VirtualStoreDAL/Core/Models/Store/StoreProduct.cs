namespace Pisa.VirtualStore.Dal.Core.Models.Store
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Pisa.VirtualStore.Dal.Core.Models.Product;

    public partial class StoreProduct
    {
        public int Id { get; set; }

        [NotMapped]
        public int IdStore { get; set; }

        [NotMapped]
        public int IdProduct { get; set; }

        [Required]
        [StringLength(20)]
        public string ProductCode { get; set; }

        public double Price { get; set; }

        public virtual Product Product { get; set; }

        public virtual Store Store { get; set; }
    }
}
