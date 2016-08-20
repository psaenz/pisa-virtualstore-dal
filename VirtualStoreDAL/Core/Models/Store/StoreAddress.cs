namespace Pisa.VirtualStore.Dal.Core.Models.Store
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Pisa.VirtualStore.Dal.Core.Models.Contact;

    public partial class StoreAddress
    {
        public int Id { get; set; }

        [NotMapped]
        public int IdContactAddress { get; set; }

        [NotMapped]
        public int IdStore { get; set; }

        public virtual ContactAddress ContactAddress { get; set; }

        public virtual Store Store { get; set; }
    }
}
