namespace Pisa.VirtualStore.Dal.Core.Models.Security
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Pisa.VirtualStore.Dal.Core.Models.Archived;
    using Pisa.VirtualStore.Dal.Core.Models.Client;
    using Pisa.VirtualStore.Dal.Core.Models.Calculus;
    using Pisa.VirtualStore.Dal.Core.Models.Contact;
    using Pisa.VirtualStore.Dal.Core.Models.General;
    using Pisa.VirtualStore.Dal.Core.Models.Offer;
    using Pisa.VirtualStore.Dal.Core.Models.Order;
    using Pisa.VirtualStore.Dal.Core.Models.Product;
    using Pisa.VirtualStore.Dal.Core.Models.Security;
    using Pisa.VirtualStore.Dal.Core.Models.Service;
    using Pisa.VirtualStore.Dal.Core.Models.Store;

    public partial class SecurityAccount : BaseAuditableModel
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SecurityAccount()
        {
            SecurityAccountUsers = new HashSet<SecurityAccountUser>();
        }

        [Key]
        public int Id { get; set; }

        [NotMapped]
        public int IdSecurityUserOwner { get; set; }


        [NotMapped]
        public int IdGeneralStatus { get; set; }

        
        public SecurityUser SecurityUserOwner { get; set; }

        public GeneralStatus GeneralStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SecurityAccountUser> SecurityAccountUsers { get; set; }
    }
}
