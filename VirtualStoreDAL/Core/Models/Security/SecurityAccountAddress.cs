namespace Pisa.VirtualStore.Dal.Core.Models.Security
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Pisa.VirtualStore.Dal.Core.Models.Contact;

    public partial class SecurityAccountAddress : BaseAuditableModel
    {
        public int Id { get; set; }

        [NotMapped]
        public int IdContactAddress { get; set; }

        [NotMapped]
        public int SecurityPersonId { get; set; }

        public virtual ContactAddress ContactAddress { get; set; }

        public virtual SecurityAccount SecurityAccount { get; set; }
    }
}
