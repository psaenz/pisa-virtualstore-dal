namespace Pisa.VirtualStore.Dal.Core.Models.Audit
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Pisa.VirtualStore.Dal.Core.Models.Security;

    public partial class AuditAuthor 
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AuditAuthor()
        {
        }

        [Key]
        public int Id { get; set; }

        // Dont add the SecurityUser here - we dont actually need it, and will create a cycle reference.
        // The lighter this class is the better
        // When it is -1 means it was created by the system, for instance Unit Text or the Feed method in the VirtualStoreDbInitializer
        public int IdSecurityUser { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime? CurrentDate { get; set; }
    }
}
