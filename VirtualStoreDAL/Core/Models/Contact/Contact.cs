namespace Pisa.VirtualStore.Dal.Core.Models.Contact
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Contact
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Contact()
        {
        }

        public int Id { get; set; }

        [NotMapped]
        public int IdContactType { get; set; }

        [Column("Description")]
        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public virtual ContactType ContactType { get; set; }
    }
}



