namespace Pisa.VirtualStore.Dal.Core.Models.Security
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SecurityPerson : BaseAuditableModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SecurityPerson()
        {
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MaidenName { get; set; }

        public string Email { get; set; }

        public bool EmailVerified { get; set; }
    }
}
