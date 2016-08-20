namespace Pisa.VirtualStore.Dal.Core.Models.Contact
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Pisa.VirtualStore.Dal.Core.Models.Order;
    using Pisa.VirtualStore.Dal.Core.Models.Store;
    using Pisa.VirtualStore.Dal.Core.Models.General;

    public partial class ContactRegion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ContactRegion()
        {
            ContactAddresses = new HashSet<ContactAddress>();
            Orders = new HashSet<Order>();
            StoreZones = new HashSet<StoreZone>();
            ContactRegions = new HashSet<ContactRegion>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [NotMapped]
        public int? IdContactRegionParent { get; set; }

        [NotMapped]
        public int IdGeneralStatus { get; set; }

        public virtual ContactRegion ContactRegionParent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContactAddress> ContactAddresses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreZone> StoreZones { get; set; }

        public virtual GeneralStatus GeneralStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContactRegion> ContactRegions { get; set; }

        
    }
}
