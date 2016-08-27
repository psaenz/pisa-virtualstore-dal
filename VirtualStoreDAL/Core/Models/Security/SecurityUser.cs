namespace Pisa.VirtualStore.Dal.Core.Models.Security
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Pisa.VirtualStore.Dal.Core.Models.General;

    public partial class SecurityUser : BaseAuditableModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SecurityUser()
        {
        }

        public int Id { get; set; }

        public int IdSecurityPerson { get; set; }

        public int IdGeneralStatus { get; set; }

        public int IdLastAccountUsed { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public bool MustChangeThePassword { get; set; }

        public virtual GeneralStatus GeneralStatus { get; set; }

        public virtual SecurityPerson SecurityPerson { get; set; }

        /// <summary>
        /// When a user is invited by an Store to register, and later it user is removed we need
        /// to tell the user from which account he was removed and let them create a new Account
        /// </summary>
        public virtual SecurityAccount LastAccountUsed { get; set; }
    }
}
