namespace Pisa.VirtualStore.Dal.Core.Models.Security
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Pisa.VirtualStore.Dal.Core.Models.General;

    public partial class SecurityAction : BaseModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SecurityAction()
        {
            SecurityActions = new HashSet<SecurityAction>();
            SecurityProfileActions = new HashSet<SecurityProfileAction>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        [Required]
        [StringLength(140)]
        public string Description { get; set; }

        [NotMapped]
        public int? IdSecurityActionParent { get; set; }

        [Required]
        [StringLength(150)]
        public string ActionToExecute { get; set; }

        public int Order { get; set; }

        /// <summary>
        /// Use to disable an option for all the profiles and users
        /// </summary>
        public GeneralStatus GeneralStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SecurityAction> SecurityActions { get; set; }

        public virtual SecurityAction SecurityActionParent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SecurityProfileAction> SecurityProfileActions { get; set; }
    }
}
