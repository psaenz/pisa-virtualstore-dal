namespace Pisa.VirtualStore.Dal.Core.Models.Offer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Pisa.VirtualStore.Dal.Core.Models.Product;

    public partial class OffersDetail
    {
        public int Id { get; set; }

        [NotMapped]
        public int IdOffer { get; set; }

        [NotMapped]
        public int IdProduct { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public virtual Offer Offer { get; set; }

        public virtual Product Product { get; set; }
    }
}
