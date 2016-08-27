namespace Pisa.VirtualStore.Dal.Core.Models.Security
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SecurityPassword : BaseAuditableModel
    {
        public int Id { get; set; }

        [NotMapped]
        public int IdSecurityUser { get; set; }

        public int Sequence { get; set; }

        [Required]
        [StringLength(150)]
        public string Password { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateCreated { get; set; }

        public virtual SecurityUser SecurityUser { get; set; }
    }
}
