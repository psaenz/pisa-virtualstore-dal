namespace Pisa.VirtualStore.Dal.Core.Models.Security
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Pisa.VirtualStore.Dal.Core.Models.General;

    public enum SecurityProfileType : int
    {
        Administrator = 10,
        Client = 20,
        Store = 30,
        Brand = 40,
        Provider = 50
    }

    public partial class SecurityProfile : BaseAuditableModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SecurityProfile()
        {
            SecurityProfileActions = new HashSet<SecurityProfileAction>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        [NotMapped]
        public int IdSecurityProfileParent { get; set; }

        [NotMapped]
        public int IdStatus { get; set; }

        public SecurityProfileType SecurityProfileType { get; set; }

        public virtual GeneralStatus GeneralStatus { get; set; }

        public virtual SecurityProfile SecurityProfileParent { get; set; }

        public virtual SecurityAccount SecurityAccountCreator { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SecurityProfileAction> SecurityProfileActions { get; set; }
    }
}
