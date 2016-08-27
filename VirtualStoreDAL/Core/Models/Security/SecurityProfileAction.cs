namespace Pisa.VirtualStore.Dal.Core.Models.Security
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SecurityProfileAction : BaseAuditableModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SecurityProfileAction()
        {
        }

        public int Id { get; set; }

        [NotMapped]
        public int IdSecurityProfile { get; set; }

        [NotMapped]
        public int IdSecurityAction { get; set; }

        public bool Available { get; set; }

        public virtual SecurityAction SecurityAction { get; set; }

        public virtual SecurityProfile SecurityProfile { get; set; }
    }
}
