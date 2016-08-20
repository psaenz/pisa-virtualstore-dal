namespace Pisa.VirtualStore.Dal.Core.Models.Archived
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Pisa.VirtualStore.Dal.Core.Models;
    using Pisa.VirtualStore.Dal.Core.Models.Archived;
    using Pisa.VirtualStore.Dal.Core.Models.Calculus;
    using Pisa.VirtualStore.Dal.Core.Models.Contact;
    using Pisa.VirtualStore.Dal.Core.Models.Client;
    using Pisa.VirtualStore.Dal.Core.Models.Offer;
    using Pisa.VirtualStore.Dal.Core.Models.Order;
    using Pisa.VirtualStore.Dal.Core.Models.Service;
    using Pisa.VirtualStore.Dal.Core.Models.Store;
    using Pisa.VirtualStore.Dal.Core.Models.Security;

    public partial class ArchivedCalculusAppliedOffer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ArchivedCalculusAppliedOffer()
        {
            ArchivedCalculusBasketDetails = new HashSet<ArchivedCalculusBasketDetail>();
        }

        public int Id { get; set; }

        [NotMapped]
        public int IdCalculusOrder { get; set; }

        [NotMapped]
        public int IdOffer { get; set; }

        public int NumberApplied { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArchivedCalculusBasketDetail> ArchivedCalculusBasketDetails { get; set; }

        public virtual ArchivedCalculusOrder ArchivedCalculusOrder { get; set; }

        public virtual Offer Offer { get; set; }
    }
}
