namespace Pisa.VirtualStore.Dal.Core.Models.Archived
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Pisa.VirtualStore.Dal.Core.Models.Product;

    public partial class ArchivedCalculusFreeProduct
    {
        public int Id { get; set; }

        [NotMapped]
        public int IdArchivedCalculusOrder { get; set; }

        [NotMapped]
        public int IdProduct { get; set; }

        public int Quantity { get; set; }

        [Column("IdArchivedCalculusOrder")]
        public virtual ArchivedCalculusOrder ArchivedCalculusOrder { get; set; }

        [Column("IdProduct")]
        public virtual Product Product { get; set; }
    }
}
