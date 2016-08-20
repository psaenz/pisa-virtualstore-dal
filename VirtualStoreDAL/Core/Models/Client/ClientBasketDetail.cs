namespace Pisa.VirtualStore.Dal.Core.Models.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Pisa.VirtualStore.Dal.Core.Models.Product;

    public partial class ClientBasketDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClientBasketDetail()
        {
        }

        public int Id { get; set; }

        [NotMapped]
        public int IdBasket { get; set; }

        [NotMapped]
        public int IdProduct { get; set; }

        public double Quantity { get; set; }

        [StringLength(150)]
        public string MoreDetails { get; set; }

        public virtual Product Product { get; set; }

        public virtual ClientBasket Basket { get; set; }
    }
}
