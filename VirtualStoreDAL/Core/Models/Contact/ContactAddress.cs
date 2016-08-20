namespace Pisa.VirtualStore.Dal.Core.Models.Contact
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Pisa.VirtualStore.Dal.Core.Models.Security;
    using Pisa.VirtualStore.Dal.Core.Models.Store;

    public partial class ContactAddress
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ContactAddress()
        {
            SecurityPersons = new HashSet<SecurityPerson>();
            StoreAddresses = new HashSet<StoreAddress>();
        }

        public int Id { get; set; }

        [NotMapped]
        public int IdContactRegion { get; set; }

        [Required]
        [StringLength(250)]
        public string Details { get; set; }

        public virtual ContactRegion ContactRegion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SecurityPerson> SecurityPersons { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreAddress> StoreAddresses { get; set; }
    }
}
