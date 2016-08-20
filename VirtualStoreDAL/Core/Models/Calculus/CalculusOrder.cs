namespace Pisa.VirtualStore.Dal.Core.Models.Calculus
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Pisa.VirtualStore.Dal.Core.Models.Store;
    using Pisa.VirtualStore.Dal.Core.Models.Order;

    public partial class CalculusOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CalculusOrder()
        {
            CalculusBasketDetails = new HashSet<CalculusBasketDetail>();
            CalculusFreeProducts = new HashSet<CalculusFreeProduct>();
            CalculusAppliedOffers = new HashSet<CalculusAppliedOffer>();
            CalculusServicesCosts = new HashSet<CalculusServiceCost>();
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
        public virtual ICollection<CalculusBasketDetail> CalculusBasketDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CalculusFreeProduct> CalculusFreeProducts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CalculusAppliedOffer> CalculusAppliedOffers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CalculusServiceCost> CalculusServicesCosts { get; set; }
    }
}
