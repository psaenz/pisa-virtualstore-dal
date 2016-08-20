namespace Pisa.VirtualStore.Dal.Core.Models.Calculus
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Pisa.VirtualStore.Dal.Core.Models.Offer;

    public partial class CalculusAppliedOffer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CalculusAppliedOffer()
        {
            CalculusBasketDetails = new HashSet<CalculusBasketDetail>();
        }

        public int Id { get; set; }

        [NotMapped]
        public int IdCalculusOrder { get; set; }

        [NotMapped]
        public int IdOffer { get; set; }

        public int NumberApplied { get; set; }

        public virtual CalculusOrder CalculusOrder { get; set; }

        public virtual Offer Offer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CalculusBasketDetail> CalculusBasketDetails { get; set; }
    }
}
