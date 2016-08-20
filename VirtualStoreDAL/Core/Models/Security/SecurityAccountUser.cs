namespace Pisa.VirtualStore.Dal.Core.Models.Security
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SecurityAccountUser : BaseModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SecurityAccountUser()
        {
        }

        [Key]
        public int Id { get; set; }

        [NotMapped]
        public int IdSecurityAccount { get; set; }

        [NotMapped]
        public int IdSecurityUser { get; set; }

        [NotMapped]
        public int IdSecurityProfile { get; set; }

        public SecurityAccount SecurityAccount { get; set; }

        public SecurityUser SecurityUser { get; set; }

        public SecurityProfile SecurityProfile { get; set; }

        /// <summary>
        /// When a user has several accounts, this column will indicate which one is used as default
        /// </summary>
        public bool Default { get; set; }
    }
}
