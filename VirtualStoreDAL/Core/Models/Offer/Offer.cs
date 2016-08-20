namespace Pisa.VirtualStore.Dal.Core.Models.Offer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Pisa.VirtualStore.Dal.Core.Models.General;
    using Pisa.VirtualStore.Dal.Core.Models.Archived;
    using Pisa.VirtualStore.Dal.Core.Models.Calculus;

    public partial class Offer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Offer()
        {
            ArchivedCalculusAppliedOffers = new HashSet<ArchivedCalculusAppliedOffer>();
            CalculusAppliedOffers = new HashSet<CalculusAppliedOffer>();
            OfferDetails = new HashSet<OffersDetail>();
        }

        public int Id { get; set; }

        [NotMapped]
        public int IdStore { get; set; }

        [NotMapped]
        public int IdStatus { get; set; }

        [NotMapped]
        public int? IdGeneralMedia { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public GeneralSchedule GeneralSchedule { get; set; }

        public int MaximumPerOrder { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArchivedCalculusAppliedOffer> ArchivedCalculusAppliedOffers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CalculusAppliedOffer> CalculusAppliedOffers { get; set; }

        public virtual GeneralMedia GeneralMedia { get; set; }

        public virtual GeneralStatus GeneralStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OffersDetail> OfferDetails { get; set; }
    }
}
