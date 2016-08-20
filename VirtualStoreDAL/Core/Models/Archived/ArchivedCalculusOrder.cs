namespace Pisa.VirtualStore.Dal.Core.Models.Archived
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Pisa.VirtualStore.Dal.Core.Models.Order;
    using Pisa.VirtualStore.Dal.Core.Models.Store;

    public partial class ArchivedCalculusOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ArchivedCalculusOrder()
        {
            ArchivedCalculusBasketDetails = new HashSet<ArchivedCalculusBasketDetail>();
            ArchivedCalculusFreeProducts = new HashSet<ArchivedCalculusFreeProduct>();
            ArchivedCalculusAppliedOffers = new HashSet<ArchivedCalculusAppliedOffer>();
            ArchivedCalculusServicesCosts = new HashSet<ArchivedCalculusServiceCost>();
        }

        public int Id { get; set; }

        [NotMapped]
        public int IdOrder { get; set; }

        [NotMapped]
        public int IdStore { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CalculusDate { get; set; }

        public virtual Store Store { get; set; }

        public virtual Order Order { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArchivedCalculusBasketDetail> ArchivedCalculusBasketDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArchivedCalculusFreeProduct> ArchivedCalculusFreeProducts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArchivedCalculusAppliedOffer> ArchivedCalculusAppliedOffers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArchivedCalculusServiceCost> ArchivedCalculusServicesCosts { get; set; }
    }
}
